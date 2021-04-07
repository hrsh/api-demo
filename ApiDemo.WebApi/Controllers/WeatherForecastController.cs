using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiDemo.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherClient _client;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            WeatherClient client)
        {
            _client = client;
            _logger = logger;
        }

        [HttpGet("{city}")]
        public async Task<WeatherForecast> Get(string city)
        {
            var t = await _client.GetCurrentWeatherAsync(city);
            return new WeatherForecast
            {
                Summary = t.weather[0].description,
                TemperatureC = (int)t.main.temp,
                Date = DateTimeOffset.FromUnixTimeSeconds(t.dt).DateTime
            };
        }
    }
}
