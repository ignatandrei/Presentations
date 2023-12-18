var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.MapGet("/weatherforecast", IAsyncEnumerable<WeatherForecast> () =>
{
    return WeatherForecast.weatherForecasts();

});

app.Run();


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    static string[] summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public static async IAsyncEnumerable<WeatherForecast> weatherForecasts()
    {
        var forecast = Enumerable.Range(1, 25).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
        var i = 0;
        foreach (var item in forecast)
        {
            i++;
            if(i %10 == 0)
            {
                Console.WriteLine($"Waiting at {i}");
                await Task.Delay(5000);
            }
            yield return item;
        }
    }
}
