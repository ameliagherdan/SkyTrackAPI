using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using SkyTrackAPI.Services.Interfaces;

public class CachedWeatherService : ICachedWeatherService
{
    private readonly IWeatherApiAdapter _weatherApiAdapter;
    private readonly IDistributedCache _cache;
    private readonly ILogger<CachedWeatherService> _logger;
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(10);

    public CachedWeatherService(IWeatherApiAdapter weatherApiAdapter, IDistributedCache cache, ILogger<CachedWeatherService> logger)
    {
        _weatherApiAdapter = weatherApiAdapter;
        _cache = cache;
        _logger = logger;
    }

    public async Task<WeatherResponse> GetWeatherDataAsync(string city)
    {
        string cacheKey = $"weather-{city}";

        var cachedData = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            _logger.LogInformation($"Cache hit for city {city}");
            return JsonSerializer.Deserialize<WeatherResponse>(cachedData);
        }

        _logger.LogInformation($"Cache miss for city {city}. Fetching data from API.");
        var weatherData = await _weatherApiAdapter.GetWeatherDataAsync(city);

        if (weatherData != null)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheExpiration
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(weatherData), cacheOptions);
            _logger.LogInformation($"Weather data for {city} cached successfully.");
        }
        else
        {
            _logger.LogWarning($"Failed to fetch weather data for {city}.");
        }

        return weatherData;
    }
}