using System.Runtime.Caching;

namespace WebApiThrottle
{
    /// <summary>
    /// Stores policy in runtime cache, intended for OWIN self host.
    /// </summary>
    public class PolicyMemoryCacheRepository : IPolicyRepository
    {
        private ObjectCache memCache = MemoryCache.Default;

        public void Save(string id, ThrottlePolicy policy)
        {
            if (memCache[id] != null)
            {
                memCache[id] = policy;
            }
            else
            {
                memCache.Add(
                    id,
                    policy,
                    new CacheItemPolicy());
            }
        }

        public ThrottlePolicy FirstOrDefault(string id)
        {
            return (ThrottlePolicy)memCache[id];
        }

        public void Remove(string id)
        {
            memCache.Remove(id);
        }
    }
}
