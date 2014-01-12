using System;

namespace CacheAttributeExample.Caching
{
    /// <summary>
    /// Represents a cached object
    /// </summary>
    public class CachedObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CachedObject" /> class.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="obj">The object that needs to be cached.</param>
        /// <param name="expiration">The expiration in seconds.</param>
        public CachedObject(string key, object obj, long expiration)
        {
            ArgumentNotNull(key, "key");
            ArgumentNotNull(obj, "obj");
            ArgumentNotNull(expiration, "expiration");
            
            this.Key = key;
            this.Object = obj;
            this.Expiration = DateTime.Now.AddSeconds(expiration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedObject"/> class.
        /// </summary>
        /// <param name="obj">The object that needs to be cached.</param>
        /// <param name="attribute">The attribute that contains the caching parameters.</param>
        public CachedObject(object obj, CacheAttribute attribute)
            : this(attribute.CacheKey, obj, attribute.Expiration)
        {
            ArgumentNotNull(obj, "obj");
            ArgumentNotNull(attribute, "attribute");
        }

        /// <summary>
        /// Gets or sets the cache key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the cached object.
        /// </summary>
        public object Object { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time.
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if obj is null.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void ArgumentNotNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}