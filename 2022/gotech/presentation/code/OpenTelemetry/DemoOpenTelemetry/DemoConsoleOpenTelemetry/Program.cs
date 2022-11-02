WriteLine("Hello, World!");

#region writing to console

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

//old way
//var serviceName = Assembly.GetExecutingAssembly().GetName().Name;
var serviceName = ThisAssembly.Project.AssemblyName;
var serviceVersion = ThisAssembly.Info.Version;

// Configure important OpenTelemetry settings and the console exporter
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .AddSource(serviceName)
    .SetResourceBuilder(
        ResourceBuilder.CreateDefault()
            .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
    .AddConsoleExporter()
   .AddHttpClientInstrumentation(it =>
   {
       it.RecordException = true;
   })
    .AddJaegerExporter(c =>
    {

        var s = config.GetSection("Jaeger");

        s.Bind(c);
    })
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

var read = new SendHttpReq();
var nr = await read.SendMoreRequests();
WriteLine("received " + nr);

