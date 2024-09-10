using SkyTrackAPI.Services;
using SkyTrackAPI.Services.Interfaces;

namespace SkyTrackAPI.Tests;

public class RateLimitingServiceTests
{
    private readonly IRateLimitingService _rateLimitingService = new RateLimitingService();

    [Fact]
    public void IsRateLimitExceeded_ReturnsFalse_WhenWithinLimit()
    {
        // Arrange
        var clientIp = "127.0.0.1";

        // Act
        var result = _rateLimitingService.IsRateLimitExceeded(clientIp);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsRateLimitExceeded_ReturnsTrue_WhenLimitExceeded()
    {
        // Arrange
        var clientIp = "127.0.0.1";

        // Act
        _rateLimitingService.IsRateLimitExceeded(clientIp);
        var result = _rateLimitingService.IsRateLimitExceeded(clientIp);

        // Assert
        Assert.True(result);
    }
}