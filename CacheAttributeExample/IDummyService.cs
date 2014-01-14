namespace CacheAttributeExample
{
    /// <summary>
    ///     A dummy service interface.
    /// </summary>
    public interface IDummyService
    {
        /// <summary>
        ///     A dummy service method that is cached.
        /// </summary>
        /// <returns></returns>
        object DummyMethodCached();

        /// <summary>
        ///     A dummy service method that is cached and expires in 1 second.
        /// </summary>
        /// <returns></returns>
        object DummyMethodCachedExpireInOneSecond();

        /// <summary>
        ///     A dummy service method without caching.
        /// </summary>
        /// <returns></returns>
        object DummyMethodNoCaching();

        /// <summary>
        ///     A dummy service method with arguments that is cached.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        object DummyMethodWithArguments(int number, string text);
    }
}