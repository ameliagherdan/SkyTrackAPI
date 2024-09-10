using SkyTrackAPI.Middleware;
using SkyTrackAPI.Services;
using SkyTrackAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCacheSettings:ConnectionString"];
});

builder.Services.AddHttpClient<IWeatherApiAdapter, WeatherApiAdapter>();
builder.Services.AddScoped<ICacheService, RedisCacheService>();
builder.Services.AddScoped<ICachedWeatherService, CachedWeatherService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

builder.Services.AddSingleton<IRateLimitingService, RateLimitingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<SecurityHeadersMiddleware>();
app.UseHttpsRedirection();

app.UseMiddleware<RateLimitingMiddleware>();


app.MapControllers();
app.Run();