using LUSID.Services.Implementations;
using Moq;
using Xunit;

namespace LUSID.Services.Tests.UnitTests
{
    public class CacheServiceTests
    {
        [Fact]
        public void CacheService_AddOrUpdate_ShouldAddItemToCache()
        {
            // Arrange
            var cacheServiceInstance = CacheService<string, int>.GetInstance();
            // Act
            cacheServiceInstance.AddOrUpdate("one", 1);
            // Assert
            Assert.True(cacheServiceInstance.TryGetValue("one", out var value));
            Assert.Equal(1, value);
        }

        [Fact]
        public void CacheService_AddOrUpdate_ShouldUpdateItemInTheCache()
        {
            // Arrange
            var cacheServiceInstance = CacheService<string, int>.GetInstance();
            // Act
            cacheServiceInstance.AddOrUpdate("one", 1);
            cacheServiceInstance.AddOrUpdate("one", 1000);
            // Assert
            Assert.True(cacheServiceInstance.TryGetValue("one", out var value));
            Assert.Equal(1000, value);
        }

        [Fact]
        public void CacheService_AddOrUpdate_ShouldEvictLastAccessedItemWhenCacheIsFull()
        {
            // Arrange
            var cacheServiceInstance = CacheService<string, int>.GetInstance();
            // Act
            cacheServiceInstance.AddOrUpdate("one", 1);
            cacheServiceInstance.AddOrUpdate("two", 2);
            cacheServiceInstance.AddOrUpdate("three", 3);
            cacheServiceInstance.AddOrUpdate("Four", 4); // This should trigger eviction and callback
            // Assert
            Assert.False(cacheServiceInstance.TryGetValue("one", out _));
            Assert.True(cacheServiceInstance.TryGetValue("two", out var valueTwo));
            Assert.True(cacheServiceInstance.TryGetValue("three", out var valueThree));
            Assert.True(cacheServiceInstance.TryGetValue("Four", out var valueFour));
            Assert.Equal(2, valueTwo);
            Assert.Equal(3, valueThree);
            Assert.Equal(4, valueFour);
        }

        [Fact]
        public void CacheService_GetInstance_ShouldCreateOnlyOneInstance()
        {
            // Arrange
            var cacheServiceInstance1 = CacheService<string, int>.GetInstance();
            var cacheServiceInstance2 = CacheService<string, int>.GetInstance();

            // Assert
            Assert.Equal(cacheServiceInstance1, cacheServiceInstance2);
        }
    }
}
