using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;

namespace TestBackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestMultipleController : ControllerBase
    {
        private Tracer tracer;
        private Activity GetNewActionFromCurrent(string name,
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

            var activity = new Activity(name)            
                .SetParentId(traceId, spanId)
                .Start();

            activity.AddBaggage("MyTraceId", activity.TraceId.ToHexString());
            activity.AddBaggage("MySpanId", activity.SpanId.ToHexString());
            activity.AddTag("CallerMemberName", memberName);
            activity.AddTag("CallerFilePath", sourceFilePath);
            activity.AddTag("CallerLineNumber", sourceLineNumber.ToString());


            return activity;


        }
        public TestMultipleController(TracerFactoryBase tracerFactory)
        {
            this.tracer = tracerFactory.GetTracer("TestMultiple");
        }
        private Task WaitRandom(int val)
        {
            //var rng = new Random();
            //var val = rng.Next(1, 10);
            return Task.Delay(val * 1000);
            
        }
        [HttpGet("{id}")]
        public async Task<string> WaitFirst(string id)
        {
            
            await WaitRandom(2);
            var activity = GetNewActionFromCurrent(nameof(WaitFirst) + "_"+id);
            activity.AddTag("action", nameof(WaitFirst));

            TelemetrySpan ts;

            using (var span = tracer.StartActiveSpanFromActivity(activity.OperationName, activity, SpanKind.Client, out ts))
            {
                var together = new[]{
                    FirstAction(nameof(GetActivityFirst) + "_" + id),
                SecondAction(nameof(WaitFirst) + "_" + id)
                    };
                await Task.WhenAll(together);
                //activity.Stop();
                return $"This is " ;
                
            }
        }
        [HttpGet("{id}")]
        public async Task<string> GetActivityFirst(string id)
        {
            
            var activity = GetNewActionFromCurrent(nameof(GetActivityFirst)+"_"+id);
            activity.AddTag("action", nameof(GetActivityFirst));
            

            await WaitRandom(1);
            
            TelemetrySpan ts;

            using (var span = tracer.StartActiveSpanFromActivity(activity.OperationName, activity, SpanKind.Client, out ts))
            {
                var actDelay = Activity.Current;
                await FirstAction(nameof(GetActivityFirst) + "_" + id);
                Activity.Current = actDelay;
                await SecondAction(nameof(GetActivityFirst)+"_"+id);
                //activity.Stop();
                return "This is ";
                
            }
        }

        private async Task<int> FirstAction(string fromWhere)
        {
            await WaitRandom(3);
            var activity = GetNewActionFromCurrent(nameof(FirstAction)+ fromWhere);
            activity.AddTag("action", nameof(FirstAction));
            TelemetrySpan ts;

            using (var span = tracer.StartActiveSpanFromActivity(activity.OperationName, activity, SpanKind.Client, out ts))
            {
                //activity.Stop();
                ts.SetAttribute("I am from", nameof(FirstAction) + fromWhere);
                return 10;

            }
        }
        private async Task<int> SecondAction(string fromWhere)
        {
            await WaitRandom(5);
            var activity = GetNewActionFromCurrent(nameof(SecondAction) + fromWhere);
            activity.AddTag("action", nameof(SecondAction));
            TelemetrySpan ts;
            
            using (var span = tracer.StartActiveSpanFromActivity(activity.OperationName, activity, SpanKind.Client, out ts))
            {
                ts.SetAttribute("I am from", nameof(SecondAction) + fromWhere);
                //activity.Stop();
                return 10;

            }
        }

    }
}
