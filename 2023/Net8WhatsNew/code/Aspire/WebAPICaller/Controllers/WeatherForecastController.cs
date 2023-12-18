using Microsoft.AspNetCore.Mvc;

namespace WebAPICaller.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async IAsyncEnumerable<WeatherForecast> CallInChromeForShowJsonAsync([FromKeyedServices("HttpWeather")]HttpClient h)
    {
        Console.WriteLine("calling "+ h.BaseAddress);
        var r= h.GetFromJsonAsAsyncEnumerable<WeatherForecast>("weatherforecast");
        ArgumentNullException.ThrowIfNull(r);
        await foreach (var item in r)
        {
            if (item == null)
                continue;
            item.Summary = "From WebAPI Caller: " + item.Summary;
            item.TemperatureC += 10;
            yield return item;
        }
    }


    [HttpGet]
    public async IAsyncEnumerable<WeatherForecast> FromNodeToShowOpenTelemetry([FromKeyedServices("NodeWeather")] HttpClient h)
    {
        Console.WriteLine("calling " + h.BaseAddress);
        var r = h.GetFromJsonAsAsyncEnumerable<WeatherForecast>("/WeatherFromNode");
        ArgumentNullException.ThrowIfNull(r);
        await foreach (var item in r)
        {
            if (item == null)
                continue;
            item.Summary = "From WebAPI Caller: " + item.Summary;
            item.TemperatureC += 10;
            yield return item;
        }
    }
}
