using System.Net.Http.Json;

namespace ClientAppsIntegration.WinForms;

public class WeatherApiClient(HttpClient httpClient)
{
    public async Task<WeatherForecast[]> GetWeatherAsync(CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast", cancellationToken) ?? [];
    }

    public async Task<byte[]> GetZipFile(string text, CancellationToken cancellationToken = default)
    {
        var data = await httpClient.GetStringAsync($"/zip/{text}", cancellationToken);
        data = data.Replace("\"", string.Empty);
        var x = System.Convert.FromBase64String(data);
        return x;
    }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
