using System;

namespace CacheAttributeExample.Caching
{
    /// <summary>
    /// Method attribute indicating caching should be applied to a method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CacheAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheAttribute" /> class.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="expiration">The expiration in seconds.</param>
        public CacheAttribute(string cacheKey, long expiration)
        {
            this.CacheKey = cacheKey;
            this.Expiration = expiration;
        }

        /// <summary>
        /// Gets or sets the expiration in seconds
        /// </summary>
        public long Expiration { get; set; }

        /// <summary>
        /// Gets or sets the cache key.
        /// </summary>
        public string CacheKey { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{{ Expiration: {0}, CacheKey = \"{1}\" }}", this.Expiration, this.CacheKey);
        }
    }
}