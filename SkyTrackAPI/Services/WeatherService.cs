using SkyTrackAPI.Services.Interfaces;

namespace SkyTrackAPI.Services;

public class WeatherService(ICachedWeatherService cachedWeatherService) : IWeatherService
{
    public async Task<WeatherResponse?> GetWeatherDataAsync(string city)
    {
        var weatherData = await cachedWeatherService.GetWeatherDataAsync(city);
        
        return weatherData ?? null;
    }
}