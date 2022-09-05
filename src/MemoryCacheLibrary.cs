using Microsoft.Extensions.Caching.Memory;

namespace CacheToolkit
{
    public class MemoryCacheLibrary : ICacheService
    {
        private readonly IMemoryCache _cache;
        public MemoryCacheLibrary(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T GetObject<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public string GetStringValue(string key)
        {
            var data = _cache.Get(key);
            return data.ToString();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void SetObject(string key, object value, int? timeOutMinutes)
        {
            _cache.Set(key, value);
        }

        public void SetStringValue(string key, string value, int? timeOutMinutes)
        {
            _cache.Set<string>(key, value);
        }
    }
}