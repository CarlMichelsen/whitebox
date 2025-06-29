using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Presentation.Service;

namespace Application.Service;

public class CacheService(
    IMemoryCache memoryCache) : ICacheService
{
    public Task<T?> Get<T>(string key)
    {
        return memoryCache.TryGetValue(key, out string? value)
            ? Task.FromResult(JsonSerializer.Deserialize<T>(value!))
            : Task.FromResult<T?>(default);
    }

    public Task Set<T>(string key, T value, TimeSpan ttl)
    {
        var json = JsonSerializer.Serialize(value);
        memoryCache.Set(key, json, new MemoryCacheEntryOptions { SlidingExpiration = ttl });
        return Task.CompletedTask;
    }

    public Task Remove(string key)
    {
        memoryCache.Remove(key);
        return Task.CompletedTask;
    }
}