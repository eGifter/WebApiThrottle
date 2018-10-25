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
            var policy = (ThrottlePolicy)memCache[id];
            return policy;
        }

        public void Remove(string id)
        {
            memCache.Remove(id);
        }
    }
}
