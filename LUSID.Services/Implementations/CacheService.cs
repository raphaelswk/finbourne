using LUSID.Services.Interfaces;
using System.Collections.Concurrent;

namespace LUSID.Services.Implementations
{
    public class CacheService<TKey, TValue> : ICacheService<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, TValue> cache;
        private readonly LinkedList<TKey> accessOrder;
        private readonly int _MaxCapacity = 3;
        private readonly Action<TKey, TValue> evictionCallback;
        private static readonly object lockObject = new object();
        
        static CacheService<TKey, TValue> instance;

        public static CacheService<TKey, TValue> GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    instance ??= new CacheService<TKey, TValue>(EvictionCallback);
                }
            }
            return instance;
        }

        private CacheService(Action<TKey, TValue> evictionCallback = null)
        {
            cache = new ConcurrentDictionary<TKey, TValue>(Environment.ProcessorCount, _MaxCapacity);
            accessOrder = new LinkedList<TKey>();
            this.evictionCallback = evictionCallback;
        }

        public void AddOrUpdate(TKey key, TValue value)
        {
            lock (lockObject)
            {
                if (!cache.TryGetValue(key, out _) && cache.Count >= _MaxCapacity)
                    Evict();
                cache[key] = value;
                MoveToEndOfAccessOrder(key);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (cache.TryGetValue(key, out value))
            {
                lock (lockObject)
                    MoveToEndOfAccessOrder(key);
                return true;
            }
            return false;
        }

        private void MoveToEndOfAccessOrder(TKey key)
        {
            accessOrder.Remove(key);
            accessOrder.AddLast(key);
        }

        private void Evict()
        {
            TKey lruKey = accessOrder.First.Value;
            TValue evictedValue;
            cache.TryRemove(lruKey, out evictedValue);
            accessOrder.RemoveFirst();
            evictionCallback?.Invoke(lruKey, evictedValue);
        }

        private static void EvictionCallback(TKey key, TValue value)
        {
            Console.WriteLine($"Evicted: Key='{key}', Value='{value}'");
        }
    }
}
