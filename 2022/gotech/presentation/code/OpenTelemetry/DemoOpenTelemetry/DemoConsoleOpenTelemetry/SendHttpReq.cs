using AOPMethodsCommon;
using Scriban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleOpenTelemetry
{
    [AutoMethods(CustomTemplateFileName = "../AutoMethod.txt", MethodPrefix = "auto", template = TemplateMethod.CustomTemplateFile)]
    partial class SendHttpReq
    {

        public SendHttpReq()
        {
        }

        public async Task<int> SendMoreRequests()
        {
            using (var activity = ActivityData.AddActivity())
            {
                var t = Enumerable.Range(1, 5).Select(

                     async it =>
                    {
                        string url = $"WeatherForecast/GetData/{it}";
                        var secs = new Random(it).Next(1, it + 2) * 1000;
                        await Task.Delay(secs);
                        try
                        {
                            var data = await MakeRequest(url);
                            return data;
                        }
                        catch (Exception ex)
                        {
                            ActivityData.AddActivityException(ex)?.Dispose();
                        }
                        return 0;
                    }
                    ).ToArray();

                var res = await Task.WhenAll(t);
                return res.Sum();
            }
        }
        [AOPMarkerMethod]
        public async Task<int> MakeRequest(string name)
        {
            using var hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:5275/");
            var res = await hc.GetStringAsync(name);
            //WriteLine(res);
            WriteLine("Task " + name + " succeeded");
            return await Calculate(name, res);
        }
        [AOPMarkerMethod]
        public async Task<int> Calculate(string? name, string? data)
        {
            await Task.Delay(2000);
            return (data?.Length  ?? 10) % (name?.Length ?? 2);
        }
    }
}