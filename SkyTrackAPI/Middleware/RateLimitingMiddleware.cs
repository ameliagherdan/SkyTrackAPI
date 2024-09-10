using SkyTrackAPI.Services.Interfaces;

namespace SkyTrackAPI.Middleware;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRateLimitingService _rateLimitingService;

    public RateLimitingMiddleware(RequestDelegate next, IRateLimitingService rateLimitingService)
    {
        _next = next;
        _rateLimitingService = rateLimitingService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var clientIp = context.Connection.RemoteIpAddress.ToString();
        if (_rateLimitingService.IsRateLimitExceeded(clientIp))
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
            return;
        }

        await _next(context);
    }
}
