using LogParser.Business.Contracts.Services;

namespace LogParser.Business.Services
{
    public class GeoCacheService : CacheService, ICacheService<string>
    {
        public string this[string key]
        {
            get { return (string)Get(key); }
            set { Add(key, value); }
        }
    }
}
