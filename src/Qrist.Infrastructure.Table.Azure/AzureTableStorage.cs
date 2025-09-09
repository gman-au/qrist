using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Qrist.Infrastructure.Table.Azure.Options;
using Qrist.Interfaces;

namespace Qrist.Infrastructure.Table.Azure
{
    public class AzureTableStorage(
        IOptions<AzureTableStorageConfigurationOptions> optionsAccessor,
        ILogger<AzureTableStorage> logger)
        : IKeyValueStorage
    {
        private const long MaxValueLength = 64 * 1024;

        private readonly AzureTableStorageConfigurationOptions _options = optionsAccessor.Value;

        public async Task<string> StoreCodeDataAsync(
            string partitionKey,
            string valueData,
            CancellationToken cancellationToken)
        {
            try
            {
                if (valueData.Length > MaxValueLength)
                    throw new Exception($"Value data exceeds maximum length of {MaxValueLength} bytes.");

                var tableClient = GetTableClient();

                var rowKey = $"{Guid.NewGuid()}-{Guid.NewGuid()}";

                var entityToAdd =
                    new QrDataTableEntity
                    {
                        RowKey = rowKey,
                        PartitionKey = partitionKey.ToUpper(),
                        RequestData = valueData
                    };

                // get existing (data would have to match)
                var queryResult =
                    tableClient
                        .Query<QrDataTableEntity>(
                            o => o.RequestData == valueData,
                            cancellationToken: cancellationToken
                        );

                var existingRecord =
                    queryResult
                        .FirstOrDefault();

                if (existingRecord != null)
                    return
                        existingRecord
                            .RowKey;

                await
                    tableClient
                        .AddEntityAsync(entityToAdd, cancellationToken);

                return rowKey;
            }
            catch (Exception ex)
            {
                logger
                    .LogError("Error occurred during storage insert: {message}", ex.Message);

                throw;
            }
        }

        public async Task<string> LookupCodeDataAsync(
            string partitionKey,
            string rowKey,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var tableClient = GetTableClient();

                var queryResult =
                    tableClient
                        .GetEntityIfExists<QrDataTableEntity>(
                            partitionKey.ToUpper(),
                            rowKey,
                            cancellationToken: cancellationToken
                        );

                if (queryResult?.Value == null)
                    throw new Exception($"Could not find record with row key {rowKey} in Azure Table Storage.");

                return
                    queryResult
                        .Value
                        .RequestData;
            }
            catch (Exception ex)
            {
                logger
                    .LogError("Error occurred during storage lookup: {message}", ex.Message);

                throw;
            }
        }

        private TableClient GetTableClient()
        {
            try
            {
                var storageUri =
                    _options?
                        .StorageUri
                    ?? throw new Exception($"{nameof(_options.StorageUri)} not set");

                var storageAccountName =
                    _options?
                        .StorageAccountName
                    ?? throw new Exception($"{nameof(_options.StorageAccountName)} not set");

                var storageAccountKey =
                    _options?
                        .StorageAccountKey
                    ?? throw new Exception($"{nameof(_options.StorageAccountKey)} not set");

                var tableName =
                    _options?
                        .TableName
                    ?? throw new Exception($"{nameof(_options.TableName)} not set");

                var serviceClient =
                    new TableServiceClient(
                        new Uri(storageUri),
                        new TableSharedKeyCredential(storageAccountName, storageAccountKey));

                var tableClient =
                    serviceClient
                        .GetTableClient(tableName);

                return tableClient;
            }
            catch (Exception ex)
            {
                logger
                    .LogError("Error occurred while trying to connect to Azure Table Storage: {message}", ex.Message);

                throw;
            }
        }
    }
}