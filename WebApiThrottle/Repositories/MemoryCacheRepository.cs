﻿using System;
using System.Linq;
using System.Runtime.Caching;

namespace WebApiThrottle
{
    /// <summary>
    /// Stors throttle metrics in runtime cache, intented for owin self host.
    /// </summary>
    public class MemoryCacheRepository : IThrottleRepository
    {
        ObjectCache memCache = MemoryCache.Default;

        /// <summary>
        /// Insert or update
        /// </summary>
        public void Save(string id, ThrottleCounter throttleCounter, TimeSpan expirationTime)
        {
            if (memCache[id] != null)
            {
                memCache[id] = throttleCounter;
            }
            else
            {
                memCache.Add(
                    id,
                    throttleCounter, new CacheItemPolicy()
                    {
                        SlidingExpiration = expirationTime
                    });
            }
        }

        public bool Any(string id)
        {
            return memCache[id] != null;
        }

        public ThrottleCounter? FirstOrDefault(string id)
        {
            return (ThrottleCounter?)memCache[id];
        }

        public void Remove(string id)
        {
            memCache.Remove(id);
        }

        public void Clear()
        {
            foreach (string cacheKey in memCache.Where(kvp => kvp.Value is ThrottleCounter).Select(kvp => kvp.Key).ToList())
            {
                memCache.Remove(cacheKey);
            }
        }
    }
}
