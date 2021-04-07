using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ApiDemo.WebApi
{
    public class WeatherClient
    {
        private readonly HttpClient _client;

        private readonly ServiceSettings _settings;

        public WeatherClient(
            HttpClient client,
            IOptions<ServiceSettings> options)
        {
            _client = client;
            _settings = options.Value;
        }

        public record Weather(string description);

        public record Main(decimal temp);

        public record Forecast(Weather[] weather, Main main, long dt);

        public async Task<Forecast> GetCurrentWeatherAsync(string city)
        {
            var forecast = await _client.GetFromJsonAsync<Forecast>(
                $"https://{_settings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={_settings.ApiKey}&units=metric");

            return forecast;
        }
    }
}