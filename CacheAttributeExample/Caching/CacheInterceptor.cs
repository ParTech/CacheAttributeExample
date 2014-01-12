using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace CacheAttributeExample.Caching
{
    /// <summary>
    /// Implements Castle DynamicProxy IInterceptor interface for the cache handler.
    /// </summary>
    public class CacheInterceptor : IInterceptor
    {
        /// <summary>
        /// Intercept any invocations on this object and apply caching for cacheable method invocations.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            CacheAttribute attribute = invocation.Method.GetCustomAttribute<CacheAttribute>();

            // Caching should not be applied when CacheAttribute was not set on the invoked method.
            if (attribute == null)
            {
                invocation.Proceed();
                return;
            }

            // The CacheAttribute has been set for this method so we can apply caching.
            // This interceptor object must implement the ICacheHandler which we use to access the cache.
            var cacheHandler = this as ICacheHandler;

            if (cacheHandler == null)
            {
                invocation.Proceed();
                return;
            }

            // Try to retrieve the cached object and return that instead of processing the original method invocation.
            object cachedResult = cacheHandler.LoadFromCache(attribute);

            if (cachedResult == null)
            {
                // Nothing was found in the cache for this method.
                // Invoke the method and cache the result.
                invocation.Proceed();
                cacheHandler.AddToCache(invocation.ReturnValue, attribute);
            }
            else
            {
                // Return the cached result.
                invocation.ReturnValue = cacheHandler.LoadFromCache(attribute);
            }
        }
    }
}