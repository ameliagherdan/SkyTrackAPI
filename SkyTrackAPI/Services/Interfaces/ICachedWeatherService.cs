namespace SkyTrackAPI.Services.Interfaces;

public interface ICachedWeatherService
{
    Task<WeatherResponse?> GetWeatherDataAsync(string city);
}