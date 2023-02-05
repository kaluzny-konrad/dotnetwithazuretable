using Azure;
using Azure.Data.Tables;

namespace dotnetwithazuretable.Api.Entities
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    public class WeatherForecastTableEntity : WeatherForecast, ITableEntity
    {
        public WeatherForecastTableEntity()
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = "WeatherForecast";
        }

        public string RowKey { get; set; } = default!;
        public string PartitionKey { get; set; } = default!;
        public ETag ETag { get; set; } = default!;
        public DateTimeOffset? Timestamp { get; set; } = default!;

        public WeatherForecast GetEntity()
        {
            return new WeatherForecast()
            {
                Date = Date,
                TemperatureC = TemperatureC,
                Summary = Summary,
            };
        }
    }
}