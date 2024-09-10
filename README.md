# SkyTrack API

SkyTrack API is a real-time weather service that pulls data from external APIs and uses Redis to cache responses, improving performance.

## Features

- **Real-time Weather Data**: Fetches weather data from external APIs.
- **Redis Caching**: Caches responses to minimize external API calls and speed up performance.
- **Rate Limiting**: Limits the number of requests from a client to avoid overload.
- **Design Patterns**: Uses **Adapter** and **Proxy** patterns.

## Architecture

The system fetches real-time weather data from external services and caches the results in Redis to enhance performance and reduce external API calls.

### Design Patterns

- **Adapter Pattern**: Connects to external APIs, making it easy to switch between API providers.
  - **Example**: The `WeatherApiAdapter` adapts the third-party API data into the format expected by the system.
  
- **Proxy Pattern**: Adds a caching layer, so if the data is already available in Redis, it returns it without calling the external API again.
  - **Example**: The `CachedWeatherService` checks if the data is in Redis before fetching it from the API.

### Rate Limiting

- Ensures clients do not exceed a certain number of requests in a given time, reducing the risk of server overload.
