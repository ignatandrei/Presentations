namespace ConsoleTest;

internal class Employee
{
    static string url= $"http://localhost:5223/api";
    static HttpClient httpClient = new HttpClient();

    public static async Task CalculateHistoryOld()
    {
        var empId = new int[]
        {
    234,579,3423,8756
        }.ToDictionary(it => it, it => "");
        foreach (var id in empId.Keys)
        {
            var guid = await httpClient.GetStringAsync($"{url}/Employee/CalculateHistory/{id}");
            empId[id] = guid;
        }
        foreach (var item in empId)
        {
            Console.WriteLine($"=========calculating for employee {item.Key}");
            var guid = item.Value;
            var urlEmpHist = $"{url}/Employee/History/{guid}";
            var data = await httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(urlEmpHist)
            });
            switch (data.StatusCode)
            {
                case HttpStatusCode.OK:
                    var str = await data.Content.ReadAsStringAsync();
                    WriteLine("for emp " + item.Key + " history :" + str);
                    break;

                default:
                    //TODO: retry
                    WriteLine("for emp " + item.Key + " status" + data.StatusCode);
                    break;
            }

        }

    }
    public static async Task CalculateWithPolly()
    {

        var maxRetryCount = 10;
        var DurationBetweenRetries = 1;//will exponent with retry count!
        var retryPolicy = Policy
                    .Handle<HttpRequestException>()
                    .Or<TaskCanceledException>()
                    .OrResult<HttpResponseMessage>(response =>
                        response.StatusCode == HttpStatusCode.ServiceUnavailable ||
                        response.StatusCode == HttpStatusCode.NotFound
                    )
                    .WaitAndRetryAsync(
                        maxRetryCount,
                        retryCount => TimeSpan.FromSeconds(DurationBetweenRetries * Math.Pow(2, retryCount - 1)),
                        (del, time) => {
                            WriteLine($" after {time.Seconds} received " + del.Result.StatusCode);
                        }
                    );




        //start empid calculating history

        var empId = new int[]
        {
    234,579,3423,8756
        }.ToDictionary(it => it, it => "");
        foreach (var id in empId.Keys)
        {
            var guid = await httpClient.GetStringAsync($"{url}/Employee/CalculateHistory/{id}");
            empId[id] = guid;
        }
        foreach (var item in empId)
        {
            Console.WriteLine($"=========Polly calculating for employee {item.Key}");

            var guid = item.Value;
            var urlEmpHist = $"{url}/Employee/History/{guid}";

            using var response = await retryPolicy
                .ExecuteAsync(() =>
                httpClient.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(urlEmpHist)
                }));

            if (response.IsSuccessStatusCode)
            {
                var body = response.Content;
                WriteLine($" hurray , history for {item.Key} is {body}");
            }
            else
            {
                WriteLine("wow .... something really bad occurs if after retries is still wrong!");
            }

        }
    }
}
