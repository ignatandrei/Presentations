using OpenTelemetry.Trace;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AT_BL
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class PersonRepository
    {
        private readonly TracerProvider tp;

        public PersonRepository(TracerProvider tp)
        {
            this.tp = tp;
        }
        public async Task<Person> GetFromId(int id)
        {
            var a = Activity.Current;
            var s = Tracer.CurrentSpan;
            var t = TracerProvider.Default.GetTracer("ad");
            var s21 = tp.GetTracer("test");
            using var s3 = s21.StartActiveSpan("my test", SpanKind.Internal);

            using var s1 = t.StartActiveSpan("asd");
            
            //var q1 = s.Start();
            //var b = q1.SpanId == s.SpanId;
            //var as2 = new ActivitySource("Samples.SampleServer");

            //using (var as1 = new Activity("getfromid"))

            //using (var q = as2.StartActivity("test", ActivityKind.Server))
            //{

            //as1.SetTag("ASDASD","asd");
            //    as1.SetParentId(s.TraceId,s.SpanId);
            //    as1.Start();
            //  q.Start();
            await Task.Delay(1000);
            return new Person()
            {
                ID = id,
                Name = $"Person with id {id}"
            };

        }
    }

}

