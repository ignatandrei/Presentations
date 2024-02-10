
using System.Text;

namespace ZipAsAService.Web;

public class WeatherApiClient(HttpClient httpClient)
{
    public async Task<WeatherForecast[]> GetWeatherAsync()
    {
        return await httpClient.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast") ?? [];
    }

    public async Task<byte[]> GetZipFile(string text)
    {
        var data= await httpClient.GetStringAsync($"/zip/{text}");
        data=data.Replace("\"", string.Empty);
        var x= System.Convert.FromBase64String(data);
        return x;
        //var x= await httpClient.GetByteArrayAsync($"/zip/{text}");
        //string base64String = Encoding.UTF8.GetString(x, 0, x.Length);
        //var data= System.Convert.FromBase64String(base64String);
        return x;
        //return data;
        //return await httpClient.GetByteArrayAsync($"/zip/{text}") ?? [];
    }

}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
