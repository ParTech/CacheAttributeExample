using System;
using CacheAttributeExample.Caching;
using Castle.DynamicProxy;

namespace CacheAttributeExample
{
    /// <summary>
    /// A dummy service class
    /// </summary>
    public class DummyService : IDummyService
    {
        private static readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        /// <summary>
        /// Initializes a new cache enabled instance of the <see cref="DummyService"/> class.
        /// </summary>
        /// <returns></returns>
        public static DummyService Create()
        {
            return Create(new CacheHandler());
        }

        /// <summary>
        /// Initializes a new cache enabled instance of the <see cref="DummyService"/> class.
        /// </summary>
        /// <param name="cacheHandler"></param>
        /// <returns></returns>
        public static DummyService Create(ICacheHandler cacheHandler)
        {
            if (!(cacheHandler is IInterceptor))
            {
                throw new Exception("Invalid CacheHandler. It must implement the IInterceptor interface to support caching.");
            }

            // Create a DummyService proxy that uses the cacheHandler as interceptor.
            var proxy = proxyGenerator.CreateClassProxy<DummyService>((IInterceptor)cacheHandler);

            return proxy;
        }

        /// <summary>
        /// A dummy service method that is cached.
        /// </summary>
        /// <returns></returns>
        [Cache("DummyService.DummyMethodCached", 300)]
        public virtual object DummyMethodCached()
        {
            string result = string.Concat("DummyMethodCached: Return value created on tick ", DateTime.Now.Ticks);

            return result;
        }

        /// <summary>
        /// A dummy service method that is cached and expires in 1 second.
        /// </summary>
        /// <returns></returns>
        [Cache("DummyService.DummyMethodCachedExpireInOneSecond", 1)]
        public virtual object DummyMethodCachedExpireInOneSecond()
        {
            string result = string.Concat("DummyMethodCachedExpireInOneSecond: Return value created on tick ", DateTime.Now.Ticks);

            return result;
        }

        /// <summary>
        /// A dummy service method without caching.
        /// </summary>
        /// <returns></returns>
        public virtual object DummyMethodNoCaching()
        {
            string result = string.Concat("DummyMethodNoCaching: Return value created on tick ", DateTime.Now.Ticks);

            return result;
        }
    }
}