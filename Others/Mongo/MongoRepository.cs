using CM.Shared.Kernel.Application.Base;
using CM.Shared.Kernel.Application.Interfaces.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Shared.Kernel.Others.Mongo
{
    public class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IMongoDatabase _database;

        private readonly IMongoCollection<T> Entities;

        public MongoRepository(MongoSettings mongoSettings, MongoContextSettings mongoContextSettings)
        {
            var client = new MongoClient(mongoSettings.ConnectionString);

            if (client != null)
                _database = client.GetDatabase(mongoContextSettings.Database);

            Entities = _database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Entities.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await Entities.Find(note => note.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task InsertAsync(T entity, CancellationToken token = default(CancellationToken))
        {
            await Entities.InsertOneAsync(entity, cancellationToken: token);
        }

        public async Task UpdateAsync(T entity, CancellationToken token = default(CancellationToken))
        {
            ReplaceOneResult actionResult = await Entities
                .ReplaceOneAsync(n => n.Id.Equals(entity.Id), entity, new UpdateOptions { IsUpsert = true });
        }

        public async Task DeleteAsync(T entity, CancellationToken token = default(CancellationToken))
        {
            DeleteResult actionResult = await Entities.DeleteOneAsync(Builders<T>.Filter.Eq("Id", entity.Id));
        }

        public Task SaveChangesAsync(CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        private ObjectId GetInternalId(Guid id)
        {
            ObjectId internalId;

            if (!ObjectId.TryParse(id.ToString(), out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }
    }
}