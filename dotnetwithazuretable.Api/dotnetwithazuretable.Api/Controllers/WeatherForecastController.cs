using Azure.Data.Tables;
using dotnetwithazuretable.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace dotnetwithazuretable.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly TableClient _tableClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _tableClient = TableClientProvider.GetTableClient();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<WeatherForecast?> GetAsync()
        {
            var weatherForecast = new WeatherForecastTableEntity()
            {
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };

            try
            {
                var result = await _tableClient.AddEntityAsync(weatherForecast);
                if (result.IsError)
                {
                    _logger.LogError("AddEntityAsync operation was failed with result: {result}", result);
                    return null;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("AddEntityAsync operation was failed with exception: {ex}", ex);
                return null;
            }
            
            return weatherForecast.GetEntity();
        }
    }
}