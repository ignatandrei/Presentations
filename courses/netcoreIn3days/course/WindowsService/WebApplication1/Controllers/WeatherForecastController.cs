using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text;
namespace WebApplication1.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public string SavePerson(Person p)
        {
            
            return $"persoana {p.id} a fost salvata"; 
        }
        
        [HttpGet]
        public async Task<Results<BadRequest<string>, Ok< WeatherForecast[]>>> GetTest1(int id)
        {
            await Task.Delay(1000);
            if (id == 2)
            {
                return TypedResults.Ok(GetWeatherForecast());
            }
            else
            {
                return TypedResults.BadRequest("errror ");
            }
        }

            [HttpGet("{id}")]
        public ActionResult< WeatherForecast[]> GetTest(int id)
        {
            if (id % 2 == 0)
                return GetWeatherForecast();

            return new BadRequestResult();
            

        }
            [HttpGet]
        public WeatherForecast[] GetWeatherForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}