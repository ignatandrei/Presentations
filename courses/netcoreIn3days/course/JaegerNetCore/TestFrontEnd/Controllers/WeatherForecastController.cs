using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenTelemetry.Context;
using OpenTelemetry.Trace;
using OpenTracing;
using OpenTracing.Propagation;

namespace TestFrontEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Tracer tracer;
        private readonly HttpClient httpClient;


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(HttpClient httpClient, ILogger<WeatherForecastController> logger, TracerFactoryBase tracerFactory)
        {
            _logger = logger;
            this.tracer = tracerFactory.GetTracer("fromClient");
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("http://localhost:5000/");
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {


            var curent = Activity.Current;
            var traceId = curent.TraceId;
            var spanId = curent.SpanId;
            if (curent.Baggage.Count(it => it.Key == "MyTraceId") > 0)
            {

                traceId = ActivityTraceId.CreateFromString(curent.GetBaggageItem("MyTraceId"));
                spanId = ActivitySpanId.CreateFromString(curent.GetBaggageItem("MySpanId"));
                curent.SetParentId(traceId, spanId);
            }

            var activity = new Activity("I am from frontend")
                        .SetParentId(curent.TraceId, curent.SpanId)
                        .Start();

            activity.AddBaggage("MyTraceId", activity.TraceId.ToHexString());
            activity.AddBaggage("MySpanId", activity.SpanId.ToHexString());
            activity.AddTag("testTag", "tag");

            TelemetrySpan ts;//= tracer.StartSpanFromActivity("muop", activity);

            using (var span = tracer.StartActiveSpanFromActivity(activity.OperationName, activity, SpanKind.Client, out ts))
            {

                ts.SetAttribute("orgId", "test frontend" + DateTime.Now.Ticks);
                var response = await this.httpClient.GetStringAsync("WeatherForecast");

                activity.Stop();
                var data = JsonConvert.DeserializeObject<WeatherForecast[]>(response);
                return new[]{
                    new WeatherForecast()
                    {
                        Date=DateTime.Now,
                        TemperatureC=data.Length,
                        Summary ="http://localhost:5000/weatherforecast/"+activity.TraceId.ToHexString() + "/"+activity.SpanId.ToHexString()
                       }

                }.Union(data).ToArray();
                ;
            }


        }
    }
}