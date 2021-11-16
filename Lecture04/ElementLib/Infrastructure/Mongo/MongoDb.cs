using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ElementLib.Infrastructure.Mongo
{
    public class MongoDb<T> : IMongoDb<T> where T : class, new()
    {
        readonly IMongoClient _Client;

        public string CollectionName { get; }
        public string DatabaseName { get; }

        readonly IMongoDatabase _Db;

        protected IMongoCollection<T> Collection => _Db.GetCollection<T>(CollectionName);

        public MongoDb(IMongoClient client, string databaseName, string collectionName)
        {
            _Client = client;
            CollectionName = collectionName;
            DatabaseName = databaseName;
            _Db = _Client.GetDatabase(databaseName);
        }

        public IMongoQueryable<T> Query => Collection.AsQueryable();
        
        public T GetOne(Expression<Func<T, bool>> expression) => 
            Collection.Find(expression).SingleOrDefault();

        public async Task<T> GetOneAsync(Expression<Func<T, bool>> expression) =>
            await Collection.Find(expression).SingleOrDefaultAsync();

        public T FindOneAndUpdate(Expression<Func<T, bool>> expression, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> option) => Collection.FindOneAndUpdate(expression, update, option);

        public void UpdateOne(Expression<Func<T, bool>> expression, UpdateDefinition<T> update) => 
            Collection.UpdateOne(expression, update);

        public async Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> expression, UpdateDefinition<T> update) =>
            await Collection.UpdateOneAsync(expression, update);

        public async Task ReplaceOneAsync(Expression<Func<T, bool>> expression, T entity) =>
            await Collection.ReplaceOneAsync(expression, entity);

        public void InsertMany(IEnumerable<T> items)
        {
            Collection.InsertMany(items);
        }

        public void InsertOne(T item) => Collection.InsertOne(item);
        public async Task InsertOneAsync(T item) => await Collection.InsertOneAsync(item);

        public void DeleteOne(Expression<Func<T, bool>> expression) => Collection.DeleteOne(expression);
        public async Task DeleteOneAsync(Expression<Func<T, bool>> expression) => await Collection.DeleteOneAsync(expression);

        public void DeleteAll(Expression<Func<T, bool>> expression) => Collection.DeleteMany(expression);

    }
}
