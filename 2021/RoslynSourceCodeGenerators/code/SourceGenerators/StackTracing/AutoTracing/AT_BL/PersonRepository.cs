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
        private readonly ActivitySource tp;

        public PersonRepository(ActivitySource tp)
        {
            this.tp = tp;
            
        }
        public async Task<Person> GetFromId(int id)
        {
            using var r=tp.StartActivity("aaaaaa");
            await Task.Delay(1000);
            using var r1 = tp.StartActivity("bbb");
            return new Person()
            {
                ID = id,
                Name = $"Person with id {id}"
            };

        }
    }

}

