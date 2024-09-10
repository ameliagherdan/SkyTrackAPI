using System.Net.Http.Json;
using SkyTrackAPI.Services.Interfaces;

public class WeatherApiAdapter : IWeatherApiAdapter
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherApiAdapter(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["WeatherAPI:Key"];
    }

    public async Task<WeatherResponse?> GetWeatherDataAsync(string city)
    {
        string url = $"https://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}";
        var response = await _httpClient.GetFromJsonAsync<WeatherResponse?>(url);
        if (response == null) return null;
        
        return response;
    }
}