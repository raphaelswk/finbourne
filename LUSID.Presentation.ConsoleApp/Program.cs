using LUSID.Services.Implementations;

namespace LUSID.Presentation.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Examples usage of the CacheService

            // Creating instance
            var cacheServiceInstance1 = CacheService<int, string>.GetInstance();
            var cacheServiceInstance2 = CacheService<int, string>.GetInstance();

            // Check if Singleton Design Pattern works accordingly
            if (cacheServiceInstance1 == cacheServiceInstance2)
                Console.WriteLine("Same instance\n");

            cacheServiceInstance1.AddOrUpdate(1, "Jhon One");
            cacheServiceInstance1.AddOrUpdate(2, "Jhon Doe");
            cacheServiceInstance1.AddOrUpdate(3, "Jhon Tre");
            cacheServiceInstance1.AddOrUpdate(1, "Jhon One Second");

            // Access "1" to make it the most recently used
            if (cacheServiceInstance1.TryGetValue(1, out var value))
                Console.WriteLine("Value for key '1': " + value);

            // Add a new item, causing the eviction of the least recently used item ("2" in this case)
            cacheServiceInstance1.AddOrUpdate(4, "Jhon Four");

            // Attempting to access "2" should return false
            if (cacheServiceInstance1.TryGetValue(2, out value))
                Console.WriteLine("Value for key '2': " + value);
            else
                Console.WriteLine("Key '2' not found in the cache.");
        }
    }
}
