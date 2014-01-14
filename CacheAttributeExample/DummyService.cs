namespace CacheAttributeExample
{
    using System;
    using CacheAttributeExample.Caching;
    using Castle.DynamicProxy;

    /// <summary>
    ///     A dummy service class.
    /// </summary>
    public class DummyService : IDummyService
    {
        private static readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        /// <summary>
        ///     Initializes a new cache enabled instance of the <see cref="DummyService"/> class.
        /// </summary>
        /// <returns></returns>
        public static DummyService Create()
        {
            return Create(new CacheHandler());
        }

        /// <summary>
        ///     Initializes a new cache enabled instance of the <see cref="DummyService"/> class.
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
        ///     A dummy service method that is cached.
        /// </summary>
        /// <returns></returns>
        [Cache(300)]
        public virtual object DummyMethodCached()
        {
            string result = string.Concat("DummyMethodCached: Return value created on tick ", DateTime.Now.Ticks);

            return result;
        }

        /// <summary>
        ///     A dummy service method that is cached and expires in 1 second.
        /// </summary>
        /// <returns></returns>
        [Cache(1)]
        public virtual object DummyMethodCachedExpireInOneSecond()
        {
            string result = string.Concat("DummyMethodCachedExpireInOneSecond: Return value created on tick ", DateTime.Now.Ticks);

            return result;
        }

        /// <summary>
        ///     A dummy service method with arguments that is cached.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        [Cache(300)]
        public virtual object DummyMethodWithArguments(int number, string text)
        {
            string result = string.Format("DummyMethodWithArguments: Return object for arguments {{ number: {0}, text: \"{1}\" }} created on tick {2}",
                number,
                text,
                DateTime.Now.Ticks);

            return result;
        }

        /// <summary>
        ///     A dummy service method without caching.
        /// </summary>
        /// <returns></returns>
        public virtual object DummyMethodNoCaching()
        {
            string result = string.Concat("DummyMethodNoCaching: Return value created on tick ", DateTime.Now.Ticks);

            return result;
        }
    }
}