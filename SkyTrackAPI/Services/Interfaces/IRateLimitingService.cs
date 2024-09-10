namespace SkyTrackAPI.Services.Interfaces;

public interface IRateLimitingService
{
    bool IsRateLimitExceeded(string clientIp);
}
