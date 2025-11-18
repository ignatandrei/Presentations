
//
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);





// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddCors();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseCors(it =>
{
    it
      .AllowAnyHeader()
      .AllowAnyMethod()
      .SetIsOriginAllowed(it=>true)
      .AllowCredentials();
});

//if (app.Environment.IsDevelopment())
{
// https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0#openapi-31-support

// https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0#openapi-in-yaml

    app.MapOpenApi();
    app.MapOpenApi("/openapi/{documentName}.yaml");
}


app.MapGet("/", () => "API service is running. Navigate to /weatherforecast to see sample data.");

// https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0#populate-xml-doc-comments-into-openapi-document

app.MapGet("/weatherforecastDocumention", MyWeather.GetWeather)
    .WithName("weatherforecastDocumention");

//https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0#support-for-server-sent-events-sse
app.MapGet("/weatherSSE", (CancellationToken cancellationToken) =>
{
    return TypedResults.ServerSentEvents(MyWeather.GetWeatherForecast(cancellationToken),eventType: "weather");
});

//IAsyncEnumerable with CancellationToken support 
app.MapGet("/weatherforecast", (CancellationToken cancellationToken) => MyWeather.GetWeatherForecast(cancellationToken));

app.MapDefaultEndpoints();

app.Run();


