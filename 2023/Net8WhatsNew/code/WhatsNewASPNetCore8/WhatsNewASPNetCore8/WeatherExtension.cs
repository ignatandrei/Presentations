using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WhatsNewASPNetCore8;

public static class WeatherExtension
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    public static void RegisterWeatherEndpoints(this IEndpointRouteBuilder endpoints)
    {
        
        endpoints.MapGet("/waether/{nr}", 
            async Task<Results<Ok<WeatherForecast[]>, NotFound<string>>>
            ([FromRoute] int nr) =>
        {
            await Task.Delay(1000);
            if(nr %2 == 0)
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
            //return "Hello World";
        }).WithTags("WeatherMinimal");
    }
}
