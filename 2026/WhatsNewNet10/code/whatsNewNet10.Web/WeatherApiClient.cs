namespace whatsNewNet10.Web;

public class WeatherApiClient(HttpClient httpClient)
{
    public async IAsyncEnumerable<WeatherForecast> GetWeatherAsync(CancellationToken cancellationToken = default)
    {
        
        await foreach (var forecast in httpClient.GetFromJsonAsAsyncEnumerable<WeatherForecast>("/weatherforecast", cancellationToken))
        {            
            if (forecast is not null)
            {
                yield return forecast;
            }
            await Task.Delay(1000, cancellationToken);
        }

    }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
