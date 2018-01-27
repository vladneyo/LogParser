using System.Collections.Concurrent;

namespace LogParser.Business.Services
{
    public abstract class CacheService
    {
        private readonly ConcurrentDictionary<string, object> _cache = new ConcurrentDictionary<string, object>();

        public virtual object Get(string key)
        {
                object obj;
                _cache.TryGetValue(key, out obj);
                return obj;
        }

        public virtual void Add(string key, object value)
        {
            _cache[key] = value;
        }
    }
}
