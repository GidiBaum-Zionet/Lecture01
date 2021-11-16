using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElementLib.Enties;
using ElementLib.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ElementLib.Infrastructure.Mongo
{
    public class ElementRepository : IElementRepository
    {
        readonly MongoDb<ElementEntity> _Db;

        public ElementRepository(MongoDb<ElementEntity> mongoClient)
        {
            _Db = mongoClient;
        }

        public async Task<IList<ElementEntity>> ReadAsync() => 
            await _Db.Query.Where(e => e.Z < 100).ToListAsync();

        public Task InsertAsync(IEnumerable<ElementEntity> elements)
        {
            _Db.InsertMany(elements);

            return Task.CompletedTask;
        }

        public async Task<ElementEntity> FindAsync(string symbol) => 
            await _Db.GetOneAsync(e => e.Symbol == symbol);

        public Task DeleteAllAsync()
        {
            _Db.DeleteAll(e => true);
            return Task.CompletedTask;
        }
    }
}
