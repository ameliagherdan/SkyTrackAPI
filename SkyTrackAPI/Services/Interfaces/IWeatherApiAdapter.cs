namespace SkyTrackAPI.Services.Interfaces;

public interface IWeatherApiAdapter
{
    Task<WeatherResponse?> GetWeatherDataAsync(string city);
}