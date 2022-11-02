using Microsoft.AspNetCore.Mvc;

namespace DemoOpenTelemetry.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{nr}")]
        public async Task<WeatherForecast[]> GetData(int nr)
        {
            //not using DI for clarity
            ForeCastData forecast = new ();
            return await forecast.GetData(nr);
        }
        
        
    }
}