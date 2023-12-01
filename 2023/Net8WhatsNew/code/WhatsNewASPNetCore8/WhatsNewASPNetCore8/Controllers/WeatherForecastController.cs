using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WhatsNewASPNetCore8.Controllers;
[ApiController]
[Route("[controller]/[action]")]
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

    [HttpGet("{nr}")]
    public async Task<Results<Ok<WeatherForecast[]>, NotFound<string>>> WithTaskResults(int nr)
    {
        await Task.Delay(1000);
        if (nr % 2 == 0)
            return TypedResults.NotFound<string>($"Not found for {nr}");
        var rng = new Random();
        var forecasts = Enumerable.Range(1, nr).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        })
        .ToArray();

        return TypedResults.Ok(forecasts);
    }
    


    [ProducesResponseType<WeatherForecast[]>(StatusCodes.Status200OK)]
    [ProducesResponseType<NotFound<string>>(StatusCodes.Status404NotFound)]
    [HttpGet("{nr}")]
    public IActionResult Produces(int nr)
    {
        if (nr % 2 == 0)
            return NotFound($"Not found for {nr}");

        var fc=Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        return Ok(fc);
    }

}
