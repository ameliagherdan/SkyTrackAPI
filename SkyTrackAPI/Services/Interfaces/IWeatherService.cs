namespace SkyTrackAPI.Services.Interfaces;

public interface IWeatherService
{
    Task<WeatherResponse?> GetWeatherDataAsync(string city);
}