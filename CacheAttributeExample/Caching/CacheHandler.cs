using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CacheAttributeExample.Caching
{
    /// <summary>
    /// Implements caching on class methods
    /// </summary>
    public class CacheHandler : CacheInterceptor, ICacheHandler
    {
        /// <summary>
        /// The cache storage storage.
        /// Don't access directly, use Storage property instead!
        /// </summary>
        private static Dictionary<string, CachedObject> storage;

        /// <summary>
        /// Gets the cache storage.
        /// </summary>
        private static Dictionary<string, CachedObject> Storage
        {
            get
            {
                if (storage == null)
                {
                    storage = new Dictionary<string, CachedObject>();
                }

                return storage;
            }
        }

        /// <summary>
        /// Adds an object to the cache.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="parameters">The cache parameters.</param>
        public void AddToCache(object obj, CacheAttribute parameters)
        {
            Storage.Remove(parameters.CacheKey);
            Storage.Add(parameters.CacheKey, new CachedObject(obj, parameters));
        }

        /// <summary>
        /// Loads an object from the cache.
        /// </summary>
        /// <param name="parameters">The cache parameters.</param>
        /// <returns></returns>
        public object LoadFromCache(CacheAttribute parameters)
        {
            CachedObject cached;

            if (Storage.TryGetValue(parameters.CacheKey, out cached) && cached != null)
            {
                // Object was found in cache. Check if it has expired.
                if (cached.Expiration > DateTime.Now)
                {
                    return cached.Object;
                }
                
                // Cache has expired. Remove the object from storage.
                Storage.Remove(parameters.CacheKey);
            }

            return null;
        }
    }
}