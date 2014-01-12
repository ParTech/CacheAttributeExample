using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CacheAttributeExample.UnitTests
{
    [TestClass]
    public class DummyServiceTests
    {
        [TestMethod]
        public void CallServiceMethods()
        {
            var service = DummyService.Create();

            // Call a method that is not cached.
            Debug.WriteLine(service.DummyMethodNoCaching());

            // Call a cached method twice.
            Debug.WriteLine(service.DummyMethodCached());
            Debug.WriteLine(service.DummyMethodCached());

            // Call a method that is cached for one second 25 times 
            // with 100 miliseconds wait in between.
            for (int i = 0; i < 25; i++)
            {
                Debug.WriteLine(service.DummyMethodCachedExpireInOneSecond());
                Thread.Sleep(100);
            }
        }
    }
}