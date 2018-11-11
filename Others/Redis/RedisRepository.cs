using CM.Shared.Kernel.Application.Base;
using CM.Shared.Kernel.Application.Interfaces.Repository;
using CM.Shared.Kernel.Others.Redis;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Shared.Kernel.Others.Redis
{
    public class RedisRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IConnectionMultiplexer _redisConnection;

        private readonly string _namespace;

        public RedisRepository(RedisConnector redisConnector)
        {
            _redisConnection = redisConnector.Connection;
            _namespace = typeof(T).Name;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            List<T> items = new List<T>();
            var database = _redisConnection.GetDatabase();
            var endPoint = _redisConnection.GetEndPoints().First();
            var server = _redisConnection.GetServer(endPoint);
            var keys = server.Keys(pattern: MakeKey("*"));
            var serializedItems = await database.StringGetAsync(keys.ToArray(), CommandFlags.None);

            foreach (var item in serializedItems)
            {
                items.Add(JsonConvert.DeserializeObject<T>(item.ToString()));
            }

            return items;
        }

        public async Task<T> GetAsync(Guid id)
        {
            var key = MakeKey(id.ToString());
            var database = _redisConnection.GetDatabase();
            var serializedObject = await database.StringGetAsync(key);

            if (serializedObject.IsNullOrEmpty)
                throw new ArgumentNullException();

            return JsonConvert.DeserializeObject<T>(serializedObject.ToString());
        }

        public async Task InsertAsync(T entity, CancellationToken token = default(CancellationToken))
        {
            var key = MakeKey(entity.Id.ToString());
            var database = _redisConnection.GetDatabase();

            await database.StringSetAsync(MakeKey(key), JsonConvert.SerializeObject(entity));
        }

        public async Task UpdateAsync(T entity, CancellationToken token = default(CancellationToken))
        {
            var key = MakeKey(entity.Id.ToString());
            var database = _redisConnection.GetDatabase();

            await database.StringSetAsync(MakeKey(key), JsonConvert.SerializeObject(entity));
        }

        public async Task DeleteAsync(T entity, CancellationToken token = default(CancellationToken))
        {
            var key = MakeKey(entity.Id.ToString());
            var database = _redisConnection.GetDatabase();

            await database.KeyDeleteAsync(key);
        }

        public Task SaveChangesAsync(CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public List<T> GetMultiple(List<Guid> ids)
        {
            var database = _redisConnection.GetDatabase();
            List<RedisKey> keys = new List<RedisKey>();
            foreach (Guid id in ids)
            {
                keys.Add(MakeKey(id));
            }
            var serializedItems = database.StringGet(keys.ToArray(), CommandFlags.None);
            List<T> items = new List<T>();
            foreach (var item in serializedItems)
            {
                items.Add(JsonConvert.DeserializeObject<T>(item.ToString()));
            }
            return items;
        }

        public bool Exists(Guid id)
        {
            return Exists(id.ToString());
        }

        public bool Exists(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            var serializedObject = database.StringGet(key);
            return !serializedObject.IsNullOrEmpty;
        }
        

        private string MakeKey(Guid id)
        {
            return MakeKey(id.ToString());
        }

        private string MakeKey(string keySuffix)
        {
            if (!keySuffix.StartsWith(_namespace + ":"))
            {
                return _namespace + ":" + keySuffix;
            }
            else return keySuffix; //Key is already prefixed with namespace
        }
    }
}