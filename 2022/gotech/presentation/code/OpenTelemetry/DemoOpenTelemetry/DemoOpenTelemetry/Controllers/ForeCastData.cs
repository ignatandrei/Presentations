using AOPMethodsCommon;

namespace DemoOpenTelemetry.Controllers
{
    [AutoMethods(CustomTemplateFileName = "../AutoMethod.txt", MethodPrefix = "auto", template = TemplateMethod.CustomTemplateFile)]
    public partial class ForeCastData
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        [AOPMarkerMethod]
        public async Task<WeatherForecast[]> GetData(int nr)
        {
            if (nr % 3 == 0)
                throw new ArgumentException($"cannot have number {nr}");

            var data = Enumerable.Range(1, nr)
                .Select(async index => await FindForeCast(index))
                .ToArray();
            var ret = await Task.WhenAll(data);
            return ret;
        }
        [AOPMarkerMethod]
        public async Task<WeatherForecast> FindForeCast(int index)
        {
            await Task.Delay(Random.Shared.Next(100, 1000));
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }
    }
}
