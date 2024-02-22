using Microsoft.Extensions.Caching.Distributed;

namespace InternshipAutomation.Application.Caching;

public class CacheService(IDistributedCache cache)
{
    private readonly IDistributedCache _cache = cache;
    
    public async Task SetCache(string key, byte[] value)
    {
        await _cache.SetAsync(key,value, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
        });
    }
    
    public async Task<byte[]?> GetCache(string key)
    {
        return await _cache.GetAsync(key);
    }
    
    public async Task RemoveCache(string key)
    {
        await _cache.RemoveAsync(key);
    }
}