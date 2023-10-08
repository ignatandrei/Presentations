using System;
using System.IO;

namespace ConsoleTest;

internal class EmployeeRateLimiting
{
    static string url = $"http://localhost:5223/api";
    static HttpClient httpClient = new HttpClient();

    public static async Task CalculateHistoryOld(params int[] empId)
    {

        var tasks = empId.Select(async it =>
            {
                var urlLimit = $"{url}/Employee/GetHistoryNOW_LimitedConcurrency/{it}";
                try
                {
                    WriteLine("starting " + it);
                    HttpResponseMessage response = await httpClient.GetAsync(urlLimit);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();

                }
                catch (Exception ex)
                {
                    WriteLine($"for {it} received exception" + ex.Message);
                    return $"cannot obtain history for {it}";
                }
            })
            .ToArray();

        var str = await Task.WhenAll(tasks);
        WriteLine(string.Join(Environment.NewLine, str));

    }
    public static async Task CalculateHistoryPolly(params int[] empId)
    {
        var maxRetryCount = 10;
        var DurationBetweenRetries = 1;//will exponent with retry count!
        //same retur policy, just with HttpStatusCode.TooManyRequests 
        var retryPolicy = Policy
                    .Handle<HttpRequestException>()
                    .Or<TaskCanceledException>()
                    .OrResult<HttpResponseMessage>(response =>
                        response.StatusCode == HttpStatusCode.ServiceUnavailable ||
                        response.StatusCode == HttpStatusCode.NotFound || 
                        response.StatusCode == HttpStatusCode.TooManyRequests //this is new
                    )
                    .WaitAndRetryAsync(
                        maxRetryCount,
                        retryCount => TimeSpan.FromSeconds(DurationBetweenRetries * Math.Pow(2, retryCount - 1)),
                        (del, time) => {
                            WriteLine($" after {time.Seconds} received " + del.Result.StatusCode);
                        }
                    );
        var tasks = empId.Select(async it =>
        {
            WriteLine($"start polly for {it}");
            var urlLimit = $"{url}/Employee/GetHistoryNOW_LimitedConcurrency/{it}";
            using var response = await retryPolicy
    .ExecuteAsync(() =>
    httpClient.SendAsync(new HttpRequestMessage
    {
        Method = HttpMethod.Get,
        RequestUri = new Uri(urlLimit)
    }));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return ($" hurray , history for {it} is {body}");
            }
            else
            {
                return ("wow .... something really bad occurs if after retries is still wrong!");
            }
        }).ToArray();

        var str = await Task.WhenAll(tasks);
        WriteLine(string.Join(Environment.NewLine, str));

    }
}
