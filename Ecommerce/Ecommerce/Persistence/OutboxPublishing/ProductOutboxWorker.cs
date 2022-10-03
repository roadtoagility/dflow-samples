// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Text.Json.Nodes;
using Avro;
using Avro.Generic;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.Replication;
using Npgsql.Replication.PgOutput;
using Npgsql.Replication.PgOutput.Messages;

namespace Ecommerce.Persistence.OutboxPublishing;

// https://debezium.io/blog/2019/02/19/reliable-microservices-data-exchange-with-the-outbox-pattern/
// https://pkritiotis.io/outbox-pattern-implementation-challenges/
// https://medium.com/design-microservices-architecture-with-patterns/outbox-pattern-for-microservices-architectures-1b8648dfaa27
// https://medium.com/engineering-varo/event-driven-architecture-and-the-outbox-pattern-569e6fba7216
// https://thorben-janssen.com/outbox-pattern-hibernate/


public class ProductOutboxWorker : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly string _schemaRegistryEndpoints;
    private readonly string _brokerEndpoints;
    private SchemaRegistryConfig _schemaRegistryConfig;
    private ProducerConfig _producerConfig;
    private readonly string _connectionString;
    private LogicalReplicationConnection _dbConnection;
    private readonly PgOutputReplicationSlot _products_outbox_slot = new ("products_outbox_slot");

    public ProductOutboxWorker(ILogger<ProductOutboxWorker> logger, IServiceScopeFactory scopeFactory, IConfiguration configuration)
    {
        this._schemaRegistryEndpoints = configuration.GetSection("MessagingEndpoints:SchemaRegistry").Value;
        this._brokerEndpoints = configuration.GetSection("MessagingEndpoints:Brokers").Value;
        this._connectionString = configuration.GetConnectionString("ModelConnection");
        _scopeFactory = scopeFactory;
        this._logger = logger;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        this._producerConfig = new ProducerConfig()
        {
            BootstrapServers = this._brokerEndpoints
        };
        
        this._schemaRegistryConfig = new SchemaRegistryConfig
        {
            Url = this._schemaRegistryEndpoints
        };

        if (!cancellationToken.IsCancellationRequested)
        {
            this._dbConnection = new LogicalReplicationConnection(this._connectionString);
            await this._dbConnection.Open(cancellationToken);
        }
        
        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var systemIdentification = await this._dbConnection.IdentifySystem(cancellationToken);
            await foreach (var message in this._dbConnection.StartReplication(
                               _products_outbox_slot,
                               new PgOutputReplicationOptions("products_outbox_pub", 1)
                               , cancellationToken))
            {
                if (message.GetType() == typeof(InsertMessage))
                {
                    var insert = message as InsertMessage;
                    var eventTimeProcessingMs = new DateTimeOffset(insert.ServerClock);
                    var data = insert.NewRow.GetAsyncEnumerator(cancellationToken);
                    await data.MoveNextAsync();
                    var headerEventId = await data.Current.Get<string>(cancellationToken);
                    await data.MoveNextAsync();
                    var topicAggregationKey = await data.Current.Get<string>(cancellationToken);
                    await data.MoveNextAsync();
                    var topicNameAggregationType = await data.Current.Get<string>(cancellationToken);
                    await data.MoveNextAsync();
                    var valueEventType = await data.Current.Get<string>(cancellationToken);
                    await data.MoveNextAsync();
                    var valuePayload = await data.Current.Get<string>(cancellationToken);
                    await data.MoveNextAsync();
                    var valueDatetimeOffset = await data.Current.Get<string>(cancellationToken);                    

                    this._logger.LogInformation("Export Inserted Product Inserted {HeaderEventId} {TopicAggregationKey} {TopicNameAggregationType} {ValueEventType} {ValuePayload}"
                        , headerEventId, topicAggregationKey, topicNameAggregationType, valueEventType, valuePayload);
                    
                    var topicName = $@"{systemIdentification.DbName}.{insert.Relation.Namespace}.{topicNameAggregationType}";
                    
                    // var productCreatedSchema = (RecordSchema)RecordSchema
                    //     .Parse(File.ReadAllText("../../deploy/productaggregate-schema.json"));

                    var headerId = new Header("eventId", Guid.Parse(headerEventId).ToByteArray());
                    //body = new GenericRecord(productCreatedSchema);
                    var body = new JsonObject
                    {
                        { "eventType", valueEventType },
                        { "eventProcessingTimeMs", eventTimeProcessingMs.ToUnixTimeMilliseconds() },
                        { "payload", valuePayload }
                    };

                    using var schemaRegistry = new CachedSchemaRegistryClient(this._schemaRegistryConfig);
                    using var producer =
                        new ProducerBuilder<string, string>(this._producerConfig)
                            // .SetValueSerializer(new AvroSerializer<GenericRecord>(schemaRegistry))
                            // .SetValueSerializer(new JsonSerializer<JsonObject>(schemaRegistry))
                            .Build();
                    try
                    {
                        var outboxMessage = new Message<string, string> { Key = topicAggregationKey, Value = body.ToJsonString() ,Headers = new(){headerId}};
                        var dr = await producer.ProduceAsync(topicName, outboxMessage,cancellationToken);
                        // Console.WriteLine($"produced to: {dr.TopicPartitionOffset}");
                        this._logger.LogInformation("New topic offset from published message: {TopicPartitionOffset}"
                            , dr.TopicPartitionOffset);
                        
                        // Always call SetReplicationStatus() or assign LastAppliedLsn and LastFlushedLsn individually
                        // so that Npgsql can inform the server which WAL files can be removed/recycled.
                        this._dbConnection.SetReplicationStatus(message.WalEnd);
                    }
                    catch (ProduceException<string, JsonObject> ex)
                    {
                        // In some cases (notably Schema Registry connectivity issues), the InnerException
                        // of the ProduceException contains additional information pertaining to the root
                        // cause of the problem. This information is automatically included in the output
                        // of the ToString() method of the ProduceException, called implicitly in the below.
                        this._logger.LogError("Publishing failed: {ex}", ex);
                    }
                }
            }

            await Task.Delay(100, cancellationToken);
        }
    }
}
