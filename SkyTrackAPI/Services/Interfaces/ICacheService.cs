namespace SkyTrackAPI.Services.Interfaces;

public interface ICacheService
{
    Task<T> GetFromCacheAsync<T>(string cacheKey);
    Task SetCacheAsync<T>(string cacheKey, T value, TimeSpan expiration);
}