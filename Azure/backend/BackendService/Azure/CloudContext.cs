namespace Backend
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using System.Threading.Tasks;

    public class CloudContext
    {
        private CloudStorageAccount _storageAccount;

        public CloudContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; private set; }

        public async Task<CloudTable> Table(string partitionKey)
        {
            if (_storageAccount == null)
            {
                _storageAccount = CloudStorageAccount.Parse(ConnectionString);
            }
            var tableClient = _storageAccount.CreateCloudTableClient();
            var tableReference = tableClient.GetTableReference(partitionKey);

            await tableReference.CreateIfNotExistsAsync().ConfigureAwait(false);

            return tableReference;
        }
    }
}
