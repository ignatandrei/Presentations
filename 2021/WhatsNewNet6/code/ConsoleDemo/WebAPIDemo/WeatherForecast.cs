using System.Text.Json.Serialization;

namespace WebAPIDemo
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
    //Demo: Source generator for System.Text.Json
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(WeatherForecast))]
    internal partial class MyWeatherSourceGenerationContext : JsonSerializerContext
    {
        
    }
}