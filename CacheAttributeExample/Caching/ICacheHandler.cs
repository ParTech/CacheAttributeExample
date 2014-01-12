namespace CacheAttributeExample.Caching
{
    /// <summary>
    /// Implements caching on class methods
    /// </summary>
    public interface ICacheHandler
    {
        /// <summary>
        /// Adds an object to the cache.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="parameters">The parameters.</param>
        void AddToCache(object obj, CacheAttribute parameters);

        /// <summary>
        /// Loads an object from the cache.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        object LoadFromCache(CacheAttribute parameters);
    }
}