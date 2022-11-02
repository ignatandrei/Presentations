using System.Diagnostics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


WriteLine("Hello, World!");

#region writing to console

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();


var serviceName = "MyCompany.MyProduct.MyService";
var serviceVersion = "1.0.0";

// Configure important OpenTelemetry settings and the console exporter
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .AddSource(serviceName)
    .SetResourceBuilder(
        ResourceBuilder.CreateDefault()
            .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
    .AddConsoleExporter()
    .AddJaegerExporter
    .Build();

var MyActivitySource = new ActivitySource(serviceName);

using (var activity = MyActivitySource.StartActivity("SayHello"))
{
    activity?.SetTag("foo", 1);
    activity?.SetTag("bar", "Hello, World!");
    activity?.SetTag("baz", new int[] { 1, 2, 3 });
}
#endregion
ReadKey();



var serviceProvider = new ServiceCollection()
                   .AddLogging()
                   .AddSingleton<IConfiguration>(config)
                   .AddOpenTelemetryTracing(
                   //.Add(b =>
                   //{
                   //    b//.AddRequestAdapter()
                   //   .UseJaeger(c =>
                   //   {
                   //       var s = config.GetSection("Jaeger");

                   //       s.Bind(c);


                   //   });
                   //    var x = new Dictionary<string, object>() {
                   //         { "PC", Environment.MachineName } };
                   //    b.SetResource(new Resource(x.ToArray()));

                   //})
                   .BuildServiceProvider();