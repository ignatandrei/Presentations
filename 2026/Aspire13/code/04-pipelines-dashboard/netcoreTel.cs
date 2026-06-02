#:sdk Microsoft.NET.Sdk.Web
#:package OpenTelemetry.Extensions.Hosting@1.15.1
#:package OpenTelemetry.Exporter.OpenTelemetryProtocol@1.15.1
#:package OpenTelemetry.Instrumentation.AspNetCore@1.15.1
#:package OpenTelemetry.Instrumentation.Http@1.15.1

using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Configure OpenTelemetry to start with the host and to send logs, metrics, and
// distributed traces via OTLP.
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(serviceName: builder.Environment.ApplicationName))
    .WithTracing(t => t
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation())
    .WithMetrics(m => m
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation())
    .UseOtlpExporter();

var app = builder.Build();

app.MapGet("/", async () =>
{
    app.Logger.LogInformation("Hello World!");
    await Task.Delay(2_000); // Simulate some work.
    using var client = new HttpClient();
    var response = await client.GetAsync("https://www.bing.com/");
    await Task.Delay(2_000); // Simulate some work.
    return $"Hello World! OpenTelemetry Trace: {Activity.Current?.Id}";
});

app.Run();