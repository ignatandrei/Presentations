using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Exporter.Jaeger;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Trace.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace TestConsole
{
    /// <summary>
    /// https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.DiagnosticSource/src/ActivityUserGuide.md
    /// https://jimmybogard.com/building-end-to-end-diagnostics-visualizations-with-exporters/
    /// https://jimmybogard.com/building-end-to-end-diagnostics-user-defined-context-with-correlation-context/
    /// </summary>
    class Program
    {
        static Tracer tracer;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:5000/");

            var opt = new JaegerExporterOptions();
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var serviceProvider = new ServiceCollection()
                    .AddLogging()
                    .AddSingleton<IConfiguration>(config)
                    .AddOpenTelemetry(b =>
                    {
                        b//.AddRequestAdapter()
                       .UseJaeger(c =>
                       {
                           var s = config.GetSection("Jaeger");

                           s.Bind(c);


                       });
                        var x = new Dictionary<string, object>() {
                            { "PC", Environment.MachineName } };
                        b.SetResource(new Resource(x.ToArray()));

                    }).BuildServiceProvider();
            var f = serviceProvider.GetRequiredService<TracerFactoryBase>();
            tracer = f.GetTracer("custom");

            var activity = new Activity("I am from console").Start();

            activity.AddBaggage("MyTraceId", activity.TraceId.ToHexString());
            activity.AddBaggage("MySpanId", activity.SpanId.ToHexString());
            activity.AddTag("fromConsole", "Console");
            TelemetrySpan ts;//= tracer.StartSpanFromActivity("muop", activity);

            using (var span = tracer.StartActiveSpanFromActivity(activity.OperationName, activity, SpanKind.Client, out ts))
            {
                ts.SetAttribute("orgId", "test console" + DateTime.Now.Ticks);
                var response = await hc.GetStringAsync("WeatherForecast");
                Console.WriteLine(response);
                activity.Stop();
            }
            Console.WriteLine("waiting for jaeger to send data");
            //wait for jaeger to send data
            Console.ReadLine();
            Console.WriteLine("starting to send multiple data- please wait");

            // test multiple async
            var activityMultiple = new Activity("Multiple from console").Start();

            activityMultiple .AddBaggage("MyTraceId", activityMultiple.TraceId.ToHexString());
            activityMultiple .AddBaggage("MySpanId", activityMultiple.SpanId.ToHexString());
            activity.AddTag("fromConsole", "Console");
            TelemetrySpan tsMultiple;//= tracer.StartSpanFromActivity("muop", activity);

            using (var span = tracer.StartActiveSpanFromActivity(activityMultiple.OperationName, activityMultiple, SpanKind.Client, out tsMultiple))
            {
                tsMultiple.SetAttribute("orgId", "test multiple console" + DateTime.Now.Ticks);
                var total = MultipleRequests().GetAwaiter().GetResult();
                activity.Stop();
            }
            Console.WriteLine("sent multiple data - now look into Jaeger");
            //wait for jaeger to send data
            Console.ReadLine();

        }
        static async Task<int> MultipleRequests()
        {
            var t = Enumerable.Range(1, 10).Select(

                it =>
                {
                    string url = "api/TestMultiple/";
                    url += (it % 2 == 0) ? "WaitFirst" : "GetActivityFirst";
                    url += "/" + it;
                    return MakeRequest(url);
                }
                ).ToArray();

            var res = await Task.WhenAll(t);
            return res.Sum();

        }
        static async Task<int> MakeRequest(string name)
        {
            // uncomment next lines
            //var activity = GetNewActionFromCurrent();
            //activity.AddTag("makerequest", name);

            //TelemetrySpan ts;

            //using (var span = tracer.StartActiveSpanFromActivity(activity.OperationName, activity, SpanKind.Client, out ts))
            {

                var hc = new HttpClient();
                hc.BaseAddress = new Uri("http://localhost:5000/");
                var res = await hc.GetStringAsync(name);
                return res.Length;
            }

        }
        static private Activity GetNewActionFromCurrent(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var curent = Activity.Current;

            var traceId = curent.TraceId;
            var spanId = curent.SpanId;
            if (curent?.Baggage.Count(it => it.Key == "MyTraceId") > 0)
            {

                traceId = ActivityTraceId.CreateFromString(curent.GetBaggageItem("MyTraceId"));
                spanId = ActivitySpanId.CreateFromString(curent.GetBaggageItem("MySpanId"));
            }

            var activity = new Activity(memberName)
                .SetParentId(traceId, spanId)
                .Start();

            activity.AddBaggage("MyTraceId", activity.TraceId.ToHexString());
            activity.AddBaggage("MySpanId", activity.SpanId.ToHexString());
            activity.AddTag("CallerMemberName", memberName);
            activity.AddTag("CallerFilePath", sourceFilePath);
            activity.AddTag("CallerLineNumber", sourceLineNumber.ToString());


            return activity;


        }
    }
}