namespace ZipAsAService.Web;

public class WeatherApiClient(HttpClient httpClient)
{
    public async Task<WeatherForecast[]> GetWeatherAsync()
    {
        return await httpClient.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast") ?? [];
    }

    public async Task<byte[]> GetZipFile(string text)
    {
        return await httpClient.GetByteArrayAsync($"/zip/{text}") ?? [];
    }

}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
