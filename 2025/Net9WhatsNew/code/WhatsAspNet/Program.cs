using Microsoft.Extensions.FileProviders;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//https://devblogs.microsoft.com/dotnet/dotnet9-openapi/
//See generating at build time https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/aspnetcore-openapi
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    //default changed: openapi/v1.json
    app.MapOpenApi();
}
Console.WriteLine("dotnet publish");
Console.WriteLine("/ThisIsAsset.html");
Console.WriteLine("/wwwrootStaticFiles/thisIsStatic.html");

//for wwwroot just this , I am doing this to show the difference
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
Console.WriteLine("/ThisIsAsset.html");
Console.WriteLine("/wwwrootStaticFiles/thisIsStatic.html");


//app.UseHttpsRedirection();
//goto /swaggerSwahsbuckle
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
    options.RoutePrefix = "swaggerSwahsbuckle";
});

//scalar
//goto scalar/v1
app.MapScalarApiReference(opt =>
{
    
});

app.MapGet("/", () => TypedResults.InternalServerError("Something went wrong!"));

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
