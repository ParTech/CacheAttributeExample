namespace CacheAttributeExample.Caching
{
    using System.Collections.Generic;

    /// <summary>
    ///     Implements caching on class methods
    /// </summary>
    public interface ICacheHandler
    {
        /// <summary>
        ///     Adds an object to the cache.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="expiration">The expiration in seconds.</param>
        void AddToCache(object obj, string cacheKey, long expiration);

        /// <summary>
        ///     Loads an object from the cache.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        object LoadFromCache(string cacheKey);

        /// <summary>
        ///     Removes an object from the cache.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        void RemoveFromCache(string cacheKey);

        /// <summary>
        ///     Removes all the objects from the cache.
        /// </summary>
        void RemoveAllFromCache();

        /// <summary>
        ///     Gets a collection of all the stored cache keys.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetAllCacheKeys();

        /// <summary>
        ///     Gets a collection of all the objects that are stored in the cache.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CachedObject> GetAllCachedObjects();
    }
}