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
        public async Task<Person> GetFromId(int id)
        {
            var s = Activity.Current;
            var q1 = s.Start();
            var b = q1.SpanId == s.SpanId;
            var as2 = new ActivitySource("Samples.SampleServer");
            
            using (var as1 = new Activity("getfromid"))

            using (var q = as2.StartActivity("test", ActivityKind.Server))
            {
                
                as1.SetTag("ASDASD","asd");
                as1.SetParentId(s.TraceId,s.SpanId);
                as1.Start();
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
}
