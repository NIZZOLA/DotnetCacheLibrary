using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CacheToolkit
{
    public class RedisLibrary : ICacheService
    {
        private IDatabase _database;
        private readonly Lazy<ConnectionMultiplexer> Connection;
        public RedisLibrary(string connectionString)
        {
            //var endpoint = configuration["Cache:Redis:Endpoint"];
            //var options = ConfigurationOptions.Parse(endpoint);
            Connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString));
            _database = Connection.Value.GetDatabase();
        }

        public string GetStringValue(string key)
        {
            return _database.StringGet(key);
        }

        public T GetObject<T>(string key )
        {
            try
            {
                var value = _database.StringGet(key);
                if (!value.IsNull)
                    return JsonSerializer.Deserialize<T>(value);
                else
                {
                    return default(T);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        public void SetObject(string key, object value, int? timeOutMinutes = 60)
        {
            _database.StringSet(key, JsonSerializer.Serialize(value), new TimeSpan(0, timeOutMinutes.Value, 0));
        }

        public void SetStringValue(string key, string value, int? timeOutMinutes = 60)
        {
            _database.StringSet(key, value, new TimeSpan(0, timeOutMinutes.Value, 0));
        }

        public void Remove(string key)
        {
            _database.KeyDelete(key);
        }
    }
}
