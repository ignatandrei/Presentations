

namespace WhatsNewNet7.Controllers;
[EnableRateLimiting("fixed")]
[ApiController]
[Route("api/[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
    [HttpGet()]
    public string GetData(int i)
    {
        var dt = DateTime.Now.ToString("mm:ss");
        return $"Received {i} at {dt}";
    }
        [HttpGet()]
    public IEnumerable<WeatherForecast> GetWeather()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
    [HttpGet()]
    public IEnumerable<WeatherForecast> GetWeatherForCities([FromQuery] string[] cities)
    {
        var data = GetWeather().ToArray();
        var l = Math.Min(data.Length, cities.Length);
        while (l > 0)
        {
            l--;
            var item = data[l];
            item.Summary = $"weather for {cities[l]}";
            item.Date = DateOnly.FromDateTime(DateTime.Now);
            yield return item;

        }
    }
    [HttpGet()]
    public Results<Ok<WeatherForecast[]>,NotFound<string>> GetWeatherInRange([FromQuery] DateRange range)
    {
        var data = GetWeather().ToArray();
        data = data.Where(it =>
            (it.Date >= range.From ) &&
            (it.Date <= range.To )
            )
            .ToArray();
     
        if (data.Length == 0)
            return TypedResults.NotFound("cannot find date");
        
        return TypedResults.Ok( data);
    }
}