namespace whatsNewNet10.ApiService;
/// <summary>
/// this is a sample weather service class
/// </summary>
public class MyWeather
{
    /// <summary>
    /// Generates an array of <see cref="WeatherForecast"/> objects with random temperature and summary values.
    /// </summary>
    /// <param name="id">An identifier used in the summary string for each forecast.</param>
    /// <returns>An array of <see cref="WeatherForecast"/> objects.</returns>
    public static WeatherForecast[] GetWeather(int id)
    {
        return Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    $"This is {index} from {id}"
                ))
                .ToArray();
    }

}
/// <summary>
/// sammple weather record
/// </summary>
/// <param name="Date"></param>
/// <param name="TemperatureC"></param>
/// <param name="Summary"></param>
public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    /// <summary>
    /// Gets the temperature in degrees Fahrenheit.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

