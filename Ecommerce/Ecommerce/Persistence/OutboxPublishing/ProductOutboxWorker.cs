// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Ecommerce.Domain.Aggregates;
using Ecommerce.Persistence.Repositories;
using Ecommerce.Persistence.State;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using Npgsql.Replication;
using Npgsql.Replication.PgOutput;
using Npgsql.Replication.PgOutput.Messages;

namespace Ecommerce.Persistence.OutboxPublishing;

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
                    
                    var topicName = $@"{systemIdentification.DbName}.{insert.Relation.Namespace}.{topicNameAggregationType}";

                    this._logger.LogInformation($"Export Inserted Product Inserted {headerEventId} {topicAggregationKey} {topicNameAggregationType} {valueEventType} {valuePayload}");
                }
                

                // Always call SetReplicationStatus() or assign LastAppliedLsn and LastFlushedLsn individually
                // so that Npgsql can inform the server which WAL files can be removed/recycled.
                this._dbConnection.SetReplicationStatus(message.WalEnd);
            }

            await Task.Delay(100, cancellationToken);
        }
    }
}
