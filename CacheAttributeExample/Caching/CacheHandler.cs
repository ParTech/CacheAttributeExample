namespace CacheAttributeExample.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Implements caching on class methods.
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
        public void AddToCache(object obj, string cacheKey, long expiration)
        {
            Storage.Remove(cacheKey);
            Storage.Add(cacheKey, new CachedObject(obj, cacheKey, expiration));
        }

        /// <summary>
        /// Loads an object from the cache.
        /// </summary>
        /// <param name="parameters">The cache parameters.</param>
        /// <returns></returns>
        public object LoadFromCache(string cacheKey)
        {
            CachedObject cached;

            if (Storage.TryGetValue(cacheKey, out cached) && cached != null)
            {
                // Object was found in cache. Validate the expiration.
                if (cached.Expiration > DateTime.Now)
                {
                    return cached.Object;
                }

                // Cache has expired. Remove the object from storage
                this.RemoveFromCache(cacheKey);
            }

            return null;
        }

        /// <summary>
        ///     Removes an object from the cache.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        public void RemoveFromCache(string cacheKey)
        {
            if (Storage.ContainsKey(cacheKey))
            {
                Storage.Remove(cacheKey);
            }
        }

        /// <summary>
        ///     Removes all the objects from the cache.
        /// </summary>
        public void RemoveAllFromCache()
        {
            Storage.Keys.ToList()
                .ForEach(x => Storage.Remove(x));
        }

        /// <summary>
        /// Gets a collection of all the keys that are stored in the cache.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllCacheKeys()
        {
            return Storage.Keys;
        }

        /// <summary>
        /// Gets a collection of all the objects that are stored in the cache.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CachedObject> GetAllCachedObjects()
        {
            return Storage.Values;
        }
    }
}