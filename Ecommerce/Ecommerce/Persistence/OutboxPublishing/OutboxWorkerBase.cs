// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Npgsql.Replication;
using Npgsql.Replication.PgOutput;
using Npgsql.Replication.PgOutput.Messages;

namespace Ecommerce.Persistence.OutboxPublishing;

// https://debezium.io/blog/2019/02/19/reliable-microservices-data-exchange-with-the-outbox-pattern/
// https://pkritiotis.io/outbox-pattern-implementation-challenges/
// https://medium.com/design-microservices-architecture-with-patterns/outbox-pattern-for-microservices-architectures-1b8648dfaa27
// https://medium.com/engineering-varo/event-driven-architecture-and-the-outbox-pattern-569e6fba7216
// https://thorben-janssen.com/outbox-pattern-hibernate/


public abstract class OutboxWorkerBase : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly string _connectionString;
    private LogicalReplicationConnection _dbConnection;

    protected OutboxWorkerBase(ILogger<ProductOutboxWorker> logger, IServiceScopeFactory scopeFactory, IConfiguration configuration)
    {
        this._connectionString = configuration.GetConnectionString("ModelConnection");
        _scopeFactory = scopeFactory;
        this._logger = logger;
    }

    protected virtual async Task PrepareLogicalReplicationAsync(CancellationToken cancellationToken)
    {
        var conn = new NpgsqlConnection(this._connectionString);
        await conn.OpenAsync(cancellationToken);

        await using var checkPublication =
            new NpgsqlCommand("select exists(select 1 from pg_publication where pubname = @pubName)", conn);
        checkPublication.Parameters.Add(new NpgsqlParameter<string>("pubName", ""));

        var hasPublication = (bool) await checkPublication.ExecuteScalarAsync(cancellationToken);

        if (hasPublication == false)
        {
            string insertPub = string.Format("CREATE PUBLICATION {0} FOR TABLE (insert = '{1}')",
                "products_outbox_pub", "products_outbox"); 
            await using var insertPublication = new NpgsqlCommand(insertPub, conn);

            await insertPublication.ExecuteNonQueryAsync(cancellationToken);
        }
        
        await using var checkSlotReplication = new NpgsqlCommand("select exists(select 1 from pg_replication_slots where slot_name = @slotName)",conn);
        checkPublication.Parameters.Add(new NpgsqlParameter<string>("slotName", "products_outbox_slot"));
    
        var hasSlot = (bool) await checkSlotReplication.ExecuteScalarAsync(cancellationToken);

        if (hasSlot == false)
        {
            await using var createSlot =
                new NpgsqlCommand("SELECT * FROM pg_create_logical_replication_slot( @slotName, 'pgoutput')", conn);
            createSlot.Parameters.Add(new NpgsqlParameter<string>("slotName", "products_outbox_slot"));
            await createSlot.ExecuteNonQueryAsync(cancellationToken);            
        }
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            await PrepareLogicalReplicationAsync(cancellationToken);

            this._dbConnection = new LogicalReplicationConnection(this._connectionString);
            await this._dbConnection.Open(cancellationToken);
        }
        
        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var _products_outbox_slot = new PgOutputReplicationSlot("products_outbox_slot");
            var systemIdentification = await this._dbConnection.IdentifySystem(cancellationToken);
            
            await foreach (var message in this._dbConnection.StartReplication(
                               _products_outbox_slot,
                               new PgOutputReplicationOptions("products_outbox_pub", 2)
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

                    // this._logger.LogInformation("Export Inserted Product Inserted {HeaderEventId} {TopicAggregationKey} {TopicNameAggregationType} {ValueEventType} {ValuePayload}"
                    //     , headerEventId, topicAggregationKey, topicNameAggregationType, valueEventType, valuePayload);
                    

                    try
                    {
                        // Console.WriteLine($"produced to: {dr.TopicPartitionOffset}");
                        this._logger.LogInformation("New topic offset from published message at: {0}"
                            , new DateTimeOffset(message.ServerClock).ToUnixTimeMilliseconds());
                        
                        // Always call SetReplicationStatus() or assign LastAppliedLsn and LastFlushedLsn individually
                        // so that Npgsql can inform the server which WAL files can be removed/recycled.
                        this._dbConnection.SetReplicationStatus(message.WalEnd);
                    }
                    catch (ProduceException<string, string> ex)
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
