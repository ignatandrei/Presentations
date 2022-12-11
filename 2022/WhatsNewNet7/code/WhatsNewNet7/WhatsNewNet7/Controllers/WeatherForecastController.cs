namespace WhatsNewNet7.Controllers;

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
    public IEnumerable<WeatherForecast> GetWeatherForCities([FromQuery]string[] cities)
    {
        var data= GetWeather().ToArray();
        var l = Math.Min(data.Length, cities.Length);
        while(l>0)
        {
            l--;
            var item = data[l ];
            item.Summary = $"weather for {cities[l]}";
            item.Date = DateOnly.FromDateTime( DateTime.Now);
            yield return item;
            
        }
    }
    [HttpGet()]
    public IEnumerable<WeatherForecast> GetWeatherInRange([FromQuery] DateRange range))
    {
         //if (!ModelState.IsValid)
         //       return null;
        var data = GetWeather().ToArray();
        foreach(var item in data){
            
        }
    }
    }