namespace Backend
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.RetryPolicies;
    using Microsoft.WindowsAzure.Storage.Table;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class RepositoryBase<Entity>
        where Entity : StorageEntity, new()
    {
        private string _connectionString;
        private ConcurrentQueue<Tuple<ITableEntity, TableOperation>> _operations;
        private CloudStorageAccount _storageAccount;

        protected RepositoryBase(string tableReference)
        {
            TableName = tableReference;
            _connectionString = "DefaultEndpointsProtocol=https;AccountName=momentsmedia;AccountKey=mlEnCMJk46fyWtIN8NK3nuh3UDrEvZ+6nYuO/lw1cdKzOhlVoxG4a51utKiOv3kPxUG7/K5z3v5wITzmWk5WlA==";

            _operations = new ConcurrentQueue<Tuple<ITableEntity, TableOperation>>();
        }

        public Entity Create()
        {
            var entity = new Entity();
            entity.Tuple.PartitionKey = TableName;
            entity.Tuple.RowKey = Guid.NewGuid().ToString();

            return entity;
        }

        public void Insert<TEntity>(TEntity entity)
            where TEntity : ITableEntity
        {
            var e = new Tuple<ITableEntity, TableOperation>(entity, TableOperation.Insert(entity));

            _operations.Enqueue(e);
        }

        public string TableName { get; private set; }

        public async Task<IEnumerable<Entity>> GetAll()
        {
            TableQuery<DynamicTableEntity> query = new TableQuery<DynamicTableEntity>()
                                                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, TableName)/*,
                                                    TableOperators.And,
                                                    TableQuery.CombineFilters(
                                                        TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, "row1"),
                                                        TableOperators.Or,
                                                        TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, "row2"))*/);

            var token = new TableContinuationToken();

            var segment = await MakeTableReference().ExecuteQuerySegmentedAsync(query, token).ConfigureAwait(false);

            var results = (from fetch in segment.ToArray()
                           let result = new { Entity = new Entity(), Result = fetch }
                           select result).ToArray();

            foreach (var item in results)
            {
                item.Entity.LoadFrom(item.Result);
            }
            return results.Select(item => item.Entity).ToArray();
        }

        public virtual IEnumerable<Entity> Find(Predicate<Entity> predicate)
        {
            return new Entity[0];
        }

        public async Task<Entity> Find(string rowKey)
        {
            var retrieveOperation = TableOperation.Retrieve(TableName, rowKey);
            var retrievedResult = await MakeTableReference().ExecuteAsync(retrieveOperation).ConfigureAwait(false);

            var fetch = retrievedResult.Result as DynamicTableEntity;
            var entity = new Entity();

            entity.LoadFrom(fetch);

            return entity;
        }

        public void Execute()
        {
            var count = _operations.Count;
            var toExecute = new List<Tuple<ITableEntity, TableOperation>>();
            for (var index = 0; index < count; index++)
            {
                Tuple<ITableEntity, TableOperation> operation;
                _operations.TryDequeue(out operation);
                if (operation != null)
                    toExecute.Add(operation);
            }

            toExecute
               .GroupBy(tuple => tuple.Item1.PartitionKey)
               .ToList()
               .ForEach(g =>
               {
                   var opreations = g.ToList();

                   var batch = 0;
                   var operationBatch = GetOperations(opreations, batch);

                   while (operationBatch.Any())
                   {
                       var tableBatchOperation = MakeBatchOperation(operationBatch);

                       ExecuteBatchWithRetries(tableBatchOperation);

                       batch++;
                       operationBatch = GetOperations(opreations, batch);
                   }
               });
        }


        private async void ExecuteBatchWithRetries(TableBatchOperation tableBatchOperation)
        {
            var tableRequestOptions = MakeTableRequestOptions();

            var tableReference = MakeTableReference();

            await tableReference.ExecuteBatchAsync(tableBatchOperation).ConfigureAwait(false);
        }

        private CloudTable MakeTableReference()
        {
            if (_storageAccount == null)
            {
                _storageAccount = CloudStorageAccount.Parse(_connectionString);
            }
            var tableClient = _storageAccount.CreateCloudTableClient();
            var tableReference = tableClient.GetTableReference(TableName);

            return tableReference;
        }

        private static TableRequestOptions MakeTableRequestOptions()
        {
            return new TableRequestOptions
            {
                RetryPolicy = new ExponentialRetry(TimeSpan.FromMilliseconds(2),
                                                       100)
            };
        }

        private static TableBatchOperation MakeBatchOperation(List<Tuple<ITableEntity, TableOperation>> operationsToExecute)
        {
            var tableBatchOperation = new TableBatchOperation();
            operationsToExecute.ForEach(tuple => tableBatchOperation.Add(tuple.Item2));
            return tableBatchOperation;
        }


        private static List<Tuple<ITableEntity, TableOperation>> GetOperations( IEnumerable<Tuple<ITableEntity, TableOperation>> operations, int batch)
        {
            return operations
                .Skip(batch * BatchSize)
                .Take(BatchSize)
                .ToList();
        }

        private const int BatchSize = 100;
    }
}