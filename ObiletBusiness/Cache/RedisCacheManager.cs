using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using ObiletBusiness.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletBusiness.Cache
{
    public class RedisCacheManager : IRedisCacheManager
    {
        //private readonly ConnectionMultiplexer _connectionMultiplexer;
        //private readonly IServer _server;

        //public RedisCacheManager(string host)
        //{
        //    var options = ConfigurationOptions.Parse(host);
        //    options.ConnectRetry = 5;
        //    options.AbortOnConnectFail = false;

        //    _connectionMultiplexer = ConnectionMultiplexer.Connect(options);

        //    var endpoints = _connectionMultiplexer.GetEndPoints(true);
        //    foreach (var endpoint in endpoints)
        //    {
        //        _server = Connect().GetServer(endpoint);
        //    }
        //}

        //public ConnectionMultiplexer Connect()
        //{
        //    return _connectionMultiplexer;
        //}

        //public async Task<bool> Set<T>(string key, T data, TimeSpan? expiry)
        //{
        //    var value = JsonConvert.SerializeObject(data, new JsonSerializerSettings
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //    });
        //    return await GetDb().StringSetAsync(key, value, expiry);
        //}

        //public IDatabase GetDb(int db = 0) => Connect().GetDatabase(db);

        //public async Task<bool> Exists(string key) => await GetDb().KeyExistsAsync(key);

        //public async Task<T> GetAsync<T>(string key)
        //{
        //    var data = await GetDb().StringGetAsync(key);

        //    if (data.IsNullOrEmpty)
        //        return default(T);

        //    return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //    });
        //}

        //public async Task RemoveAsync(string key)
        //{
        //    await GetDb().KeyDeleteAsync(key, CommandFlags.FireAndForget);
        //}

        //public async Task RemoveRangeAsync(string pattern)
        //{
        //    var result = _server.Keys(pattern: pattern);

        //    foreach (var key in result)
        //    {
        //        await GetDb().KeyDeleteAsync(key, CommandFlags.FireAndForget);
        //    }
        //}

        //public void Clear()
        //{
        //    _server.FlushAllDatabases();
        //}
        private readonly IMemoryCache _memoryCache;
        public RedisCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public async Task Set<T>(string key, T value)
        {
            _memoryCache.Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                Priority = CacheItemPriority.Normal
            });
        }
    }
}
