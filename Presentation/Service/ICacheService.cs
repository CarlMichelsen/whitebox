namespace Interface.Service;

public interface ICacheService
{
    Task<T?> Get<T>(string key);
    
    Task Set<T>(string key, T value, TimeSpan ttl);
    
    Task Remove(string key);
}