namespace Presentation.Service;

public static class CacheExtensions
{
    public static async Task<T?> CacheFactory<T>(this ICacheService cache, string key, TimeSpan ttl, Func<Task<T?>> factory)
    {
        var existingValue = await cache.Get<T>(key);
        if (existingValue is not null)
        {
            return existingValue;
        }
        
        var newValue = await factory();
        if (newValue is not null)
        {
            await cache.Set(key, newValue, ttl);
        }

        return newValue;
    }
}