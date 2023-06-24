using AsyncEnumerable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoAsynEnumerable.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly MyDataContext context;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            context = new MyDataContext();
            for (int i = 0; i < 1000; i++)
            {
                var p = new PersonWithBlog();
                //p.Id = i;
                p.Name = "Andrei Ignat : " + i;
                p.Url = "http://msprogrammer.serviciipeweb.ro/";
                context.PersonWithBlog.Add(p);
                //Console.WriteLine(p.Id);
            }

            context.SaveChanges();
        }

        [HttpGet()]
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("{id}")]
        public async IAsyncEnumerable<PersonWithBlog> GetPersons(int id)
        {
            await foreach (var person in GetPerson(context, id))
            {
                yield return person;
            }
        }
        static async IAsyncEnumerable<PersonWithBlog> GetPerson(MyDataContext cnt, int from)
        {
            var query = cnt.PersonWithBlog.Where(it => it.Id > from);
            await foreach (var p in query.AsAsyncEnumerable())
            {

                Console.WriteLine($"from database {p.Name}");
                //simulate processing
                await Task.Delay(1000);
                p.Id = p.Id + 1;
                yield return p;
            }
        }


    }
}