using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenTelemetry.Context;
using OpenTelemetry.Trace;
using OpenTracing;
using OpenTracing.Propagation;

namespace TestBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        //private readonly TracerFactoryBase tracerFactory;
        private readonly Tracer tracer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, TracerFactoryBase tracerFactory)
        {
            _logger = logger;
            //this.tracerFactory = tracerFactory;
            this.tracer = tracerFactory.GetTracer("custom");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            var curent = Activity.Current;
            var traceId = curent.TraceId;
            var spanId = curent.SpanId;
            if (curent.Baggage.Count(it => it.Key == "MyTraceId") > 0)
            {

                traceId = ActivityTraceId.CreateFromString(curent.GetBaggageItem("MyTraceId"));
                spanId = ActivitySpanId.CreateFromString(curent.GetBaggageItem("MySpanId"));                
            }

            var activity = new Activity("I am from backend")
                        .SetParentId(traceId, spanId)
                        .Start();

            activity.AddBaggage("MyTraceId", activity.TraceId.ToHexString());
            activity.AddBaggage("MySpanId", activity.SpanId.ToHexString());


            activity.AddBaggage("testBag", "test!te!!TRew");
            activity.AddTag("testTag", "tag");

            TelemetrySpan ts;

            using (var span = tracer.StartActiveSpanFromActivity(activity.OperationName, activity, SpanKind.Client, out ts))
            {

                ts.SetAttribute("orgId", "test backend" + DateTime.Now.Ticks);


                var x = Enumerable.Range(1, 1).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = 10,
                    Summary = "http://localhost:5000/weatherforecast/" + activity.TraceId.ToHexString() + "/" + activity.SpanId.ToHexString()
                })
                .ToArray();
                
                activity.Stop();
                return x;
            }
        }
        [HttpGet("{trace}/{span}")]
        public string GetData(string trace, string span)
        {
            try
            {
                var traceId = ActivityTraceId.CreateFromString( trace );
                var spanId = ActivitySpanId.CreateFromString(span);

                var activity = new Activity("I !!new backend")
                           .SetParentId(traceId, spanId)
                           .Start();

                TelemetrySpan ts;

                using (var spanNew = tracer.StartActiveSpanFromActivity(activity.OperationName, activity, SpanKind.Client, out ts))
                {

                    ts.SetAttribute("orgId", "test backend" + DateTime.Now.Ticks);

                    activity.Stop();
                }
                return "10";
            }
            catch(Exception ex)
            {
                return "NOT!";
            }
        }
    }
}
