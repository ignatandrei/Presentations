using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
//Demo: file scoped namespaces
namespace WebAPIDemo.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        return data;
    }

    [HttpPost]
    public string? Data(WeatherForecast? wf)
    {
        wf = null;
        VerifyNotNull(wf);
        return wf?.ToString();
    }
    //Demo: CallerArgumentExpression
    void VerifyNotNull<T>(T value,
        [CallerMemberName] string? caller = null,
        [CallerLineNumber] int? callerLineNumber = null,
        [CallerFilePath] string? callerFilePath = null,
[CallerArgumentExpression("value")] string? nameVar = null)
    {
        if (value == null)
            throw new ArgumentException($"{nameVar} is null in function {caller} line {callerLineNumber} from file {callerFilePath}", nameVar);

    }
}
