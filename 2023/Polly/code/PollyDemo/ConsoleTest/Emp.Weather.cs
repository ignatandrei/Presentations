
using Polly.Timeout;
using System.Collections;

namespace ConsoleTest;

internal class EmpWeather
{
    static string url = $"http://localhost:5223/api";
    static HttpClient httpClient = new HttpClient();
    static string lastWeather = "this is cold from cache";
    public static async Task GetWeather(params int[] empId)
    {
        foreach (var it in empId)
        {

            var urlLimit = $"{url}/Employee/GetWeatherForEmp/{it}";
            var timeoutPolicy =
    Policy.TimeoutAsync(
        TimeSpan.FromSeconds(value: 10),
        TimeoutStrategy.Pessimistic,
        async (context, timespan, task) =>
        {

            WriteLine("timeout ");


        });

            var dataPolicy = await timeoutPolicy
                .ExecuteAndCaptureAsync(async () =>
                    {
                        return await httpClient.GetStringAsync(urlLimit);
                    });

            if (dataPolicy.Outcome == OutcomeType.Successful)
            {
                WriteLine($"real weather for  {it} is " + dataPolicy.Result);
            }
            else
            {
                WriteLine($"cache weather for  {it} is " + lastWeather);
            }

        }
    }
}
