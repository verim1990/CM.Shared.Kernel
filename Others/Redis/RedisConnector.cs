using StackExchange.Redis;

namespace CM.Shared.Kernel.Others.Redis
{
    public class RedisConnector
    {
        public RedisSettings RedisSettings { get; set; }

        public ConnectionMultiplexer Connection { get; set; }

        public RedisConnector(RedisSettings redisSettings)
        {
            RedisSettings = redisSettings;
            Connection = ConnectionMultiplexer.Connect($"{redisSettings.Host}");
        }
    }
}
