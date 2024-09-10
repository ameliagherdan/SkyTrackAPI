using Microsoft.AspNetCore.Mvc;
using Moq;
using SkyTrackAPI.Models;
using SkyTrackAPI.Services.Interfaces;

namespace SkyTrackAPI.Tests;

public class WeatherControllerTests
{
    private readonly Mock<IWeatherService> _mockWeatherService;
    private readonly WeatherController _controller;

    public WeatherControllerTests()
    {
        _mockWeatherService = new Mock<IWeatherService>();
        _controller = new WeatherController(_mockWeatherService.Object);
    }

    [Fact]
    public async Task GetWeather_ReturnsOk_WhenWeatherDataExists()
    {
        // Arrange
        var city = "Oradea";
        var expectedWeather = new WeatherResponse
        {
            Location = new Location
            {
                Name = city,
                Country = "Romania",
                Region = "Bihor",
                Localtime_Epoch = 1630950000,
                Localtime = "2024-09-10 10:00"
            },
            Current = new CurrentWeather
            {
                Temp_C = 20.5,
                Is_Day = 1,
                Wind_Kph = 10,
                Wind_Dir = "SW",
                Humidity = 60,
                Cloud = 75,
                Feelslike_C = 19.8
            }
        };

        _mockWeatherService.Setup(s => s.GetWeatherDataAsync(city)).ReturnsAsync(expectedWeather);

        // Act
        var result = await _controller.GetWeather(city);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var weatherData = Assert.IsType<WeatherResponse>(okResult.Value);
        Assert.Equal(city, weatherData.Location.Name);
        Assert.Equal(20.5, weatherData.Current.Temp_C);
    }

    [Fact]
    public async Task GetWeather_ReturnsNotFound_WhenWeatherDataIsNull()
    {
        // Arrange
        var city = "UnknownCity";
        _mockWeatherService.Setup(s => s.GetWeatherDataAsync(city)).ReturnsAsync((WeatherResponse)null!);

        // Act
        var result = await _controller.GetWeather(city);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}