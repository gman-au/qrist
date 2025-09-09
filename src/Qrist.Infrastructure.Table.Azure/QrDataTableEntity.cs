using System;
using Azure;
using Azure.Data.Tables;

namespace Qrist.Infrastructure.Table.Azure
{
    public class QrDataTableEntity : ITableEntity
    {
        public string RequestData { get; set; }

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }
    }
}