namespace Qrist.Infrastructure.Table.Azure.Options
{
    public class AzureTableStorageConfigurationOptions
    {
        public string StorageUri { get; set; }

        public string StorageAccountName { get; set; }

        public string StorageAccountKey { get; set; }

        public string TableName { get; set; }
    }
}