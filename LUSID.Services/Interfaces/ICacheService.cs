namespace LUSID.Services.Interfaces
{
    public interface ICacheService<TKey, TValue>
    {
        public void AddOrUpdate(TKey key, TValue value);
        public bool TryGetValue(TKey key, out TValue value);
    }
}
