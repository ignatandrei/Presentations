using System.Runtime.CompilerServices;

namespace whatsNewNet10.Web;

public class WeatherApiClient(HttpClient httpClient)
{
    public async IAsyncEnumerable<WeatherForecast> GetWeatherAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        
        await foreach (var forecast in httpClient.GetFromJsonAsAsyncEnumerable<WeatherForecast>("/weatherforecast", cancellationToken))
        {            
            if (forecast is not null)
            {
                yield return forecast;
            }
        }

    }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
