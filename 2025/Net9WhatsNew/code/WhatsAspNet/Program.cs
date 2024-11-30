using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
Console.WriteLine("dotnet publish");
Console.WriteLine("http://localhost:5000/ThisIsAsset.html");
Console.WriteLine("http://localhost:5000/wwwrootStaticFiles/thisIsStatic.html");

//for wwwroot just this
//app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "wwwrootStaticFiles")),
    RequestPath = "/wwwrootStaticFiles"
});
//serve from wwwroot
//Allows the developer to spend extra time during the build process to ensure that the assets are the minimum size.
//https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-9.0
app.MapStaticAssets();
Console.WriteLine("dotnet publish");
Console.WriteLine("http://localhost:5000/ThisIsAsset.html");
Console.WriteLine("http://localhost:5000/wwwrootStaticFiles/thisIsStatic.html");


//app.UseHttpsRedirection();



var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
