using SkyTrackAPI.Services.Interfaces;

namespace SkyTrackAPI.Services;

public class RateLimitingService : IRateLimitingService
{
    private readonly Dictionary<string, DateTime> _requestTimestamps = new();
    private readonly int _limitInSeconds = 60;

    public bool IsRateLimitExceeded(string clientIp)
    {
        if (_requestTimestamps.ContainsKey(clientIp) &&
            (DateTime.Now - _requestTimestamps[clientIp]).TotalSeconds < _limitInSeconds)
        {
            return true;
        }

        _requestTimestamps[clientIp] = DateTime.Now;
        return false;
    }
}
