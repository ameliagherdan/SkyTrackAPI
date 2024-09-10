using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using SkyTrackAPI.Services.Interfaces;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<RedisCacheService> _logger;

    public RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<T> GetFromCacheAsync<T>(string cacheKey)
    {
        var cachedData = await _cache.GetStringAsync(cacheKey);

        if (string.IsNullOrEmpty(cachedData))
        {
            _logger.LogInformation($"Cache miss for key: {cacheKey}");
            return default;
        }

        _logger.LogInformation($"Cache hit for key: {cacheKey}");
        return JsonSerializer.Deserialize<T>(cachedData);
    }

    public async Task SetCacheAsync<T>(string cacheKey, T value, TimeSpan expiration)
    {
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(value), cacheOptions);
    }
}