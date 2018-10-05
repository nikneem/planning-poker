using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using HexMaster.Helpers.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HexMaster.Helpers.Mongo
{
    public abstract class MongoRepositoryBase<TEntity>
    {
        private MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<TEntity> _collection;

        protected IMongoCollection<TEntity> Collection => _collection;
        protected IMongoDatabase Database => _database;

        protected MongoRepositoryBase(IOptions<MongoDbSettings> settingsOptions,  string collectionName)
        {
            var connectionSettings = settingsOptions.Value;
            var connectionString = settingsOptions.Value.GetConnectionString();
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(connectionSettings.DatabaseName);
            _collection = _database.GetCollection<TEntity>(collectionName);
        }

        public IMongoQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable().Where(predicate);
        }
    }
}
