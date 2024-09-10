using Microsoft.AspNetCore.Mvc;
using SkyTrackAPI.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city)
    {
        var weatherData = await _weatherService.GetWeatherDataAsync(city);
        if (weatherData == null)
        {
            return NotFound();
        }
        return Ok(weatherData);
    }
}