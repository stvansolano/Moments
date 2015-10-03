namespace Backend
{
    //using Microsoft.WindowsAzure.Storage;
    //using Microsoft.WindowsAzure.Storage.Table;
    using System;
    using System.Collections.Generic;

    public abstract class RepositoryBase<Entity>
        where Entity : /*TableEntity, */new()
    {
        private string _connectionString;

        protected RepositoryBase(string tableReference)
        {
            _connectionString = "DefaultEndpointsProtocol=https;AccountName=momentsmedia;AccountKey=mlEnCMJk46fyWtIN8NK3nuh3UDrEvZ+6nYuO/lw1cdKzOhlVoxG4a51utKiOv3kPxUG7/K5z3v5wITzmWk5WlA==";

            /*_table = new Lazy<CloudTable>(delegate {
                // Retrieve the storage account from the connection string.

                var storageAccount = CloudStorageAccount.Parse(_connectionString);

                var tableClient = storageAccount.CreateCloudTableClient();

                CloudTable table = tableClient.GetTableReference("Friendship");

                // Create the table if it doesn't exist.
                table.CreateIfNotExistsAsync();

                return table;
            });*/
        }
        /*
        private Lazy<CloudTable> _table { get; set; }
        protected CloudTable Table { get { return _table.Value; } }
        */
        public IEnumerable<Entity> GetAll()
        {
            return new Entity[0];
        }

        public virtual IEnumerable<Entity> Find(Predicate<Entity> predicate)
        {
            return new Entity[0];
        }
    }
}