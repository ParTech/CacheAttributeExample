namespace CacheAttributeExample.UnitTests
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Threading;
    using CacheAttributeExample.Caching;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DummyServiceTests
    {
        [TestMethod]
        public void TestCacheHandler()
        {
            // Execute service methods to generate cache.
            this.DebugServiceMethods();

            var cacheHandler = new CacheHandler();
            var cachedObjects = cacheHandler.GetAllCachedObjects();

            // Test if the cache contains expected data.
            Assert.IsNotNull(cachedObjects, "CacheHandler.GetAllCachedObjects() results should not be null.");
            Assert.AreNotEqual(0, cachedObjects.Count(), "Cache storage should contains objects.");
            Assert.AreEqual(4, cachedObjects.Count(), "Cache storage should contain 4 objects.");
            Assert.IsTrue(cachedObjects.Any(x => x.Key.Equals("CacheAttributeExample.DummyService.DummyMethodCached()")), "Expected cache key not found");
            Assert.IsTrue(cachedObjects.Any(x => x.Key.Equals("CacheAttributeExample.DummyService.DummyMethodCachedExpireInOneSecond()")), "Expected cache key not found");
            Assert.IsTrue(cachedObjects.Any(x => x.Key.Equals("CacheAttributeExample.DummyService.DummyMethodWithArguments({ number : 1, text : \"Hello world!\" })")), "Expected cache key not found");
            Assert.IsTrue(cachedObjects.Any(x => x.Key.Equals("CacheAttributeExample.DummyService.DummyMethodWithArguments({ number : 2, text : \"Hello unit test!\" })")), "Expected cache key not found");
        }

        private void DebugServiceMethods()
        {
            var service = DummyService.Create();

            // Call a method without caching.
            Debug.WriteLine(service.DummyMethodNoCaching());

            // Call a cached method twice.
            Debug.WriteLine(service.DummyMethodCached());
            Debug.WriteLine(service.DummyMethodCached());

            // Call a cached method with one second expiration 6 times with an interval of 500 miliseconds.
            for (int i = 0; i < 6; i++)
            {
                Debug.WriteLine(service.DummyMethodCachedExpireInOneSecond());
                Thread.Sleep(500);
            }

            // Call a cached method with arguments twice.
            Debug.WriteLine(service.DummyMethodWithArguments(1, "Hello world!"));
            Debug.WriteLine(service.DummyMethodWithArguments(1, "Hello world!"));

            // Call the same cached method with different arguments twice.
            Debug.WriteLine(service.DummyMethodWithArguments(2, "Hello unit test!"));
            Debug.WriteLine(service.DummyMethodWithArguments(2, "Hello unit test!"));

            // Output a list of all the stored cache keys.
            Debug.WriteLine(string.Join(Environment.NewLine, new CacheHandler().GetAllCachedObjects()
                .Select(x => string.Concat("Key: ", x.Key, " Expires on: ", x.Expiration.ToString("yyyy-MM-dd HH:mm:ss:fff")))));
        }
    }
}