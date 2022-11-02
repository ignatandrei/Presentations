using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleOpenTelemetry
{
    internal class SendHttpReq
    {

        public SendHttpReq()
        {

        }

        public async Task<int> SendMoreRequests()
        {
            var t = Enumerable.Range(1, 5).Select(

                 async it =>
                {
                    string url = $"WeatherForecast/GetData/{it}";
                    var secs = new Random(it).Next(1, it+2)*1000;
                    await Task.Delay(secs);
                    return await MakeRequest(url);
                }
                ).ToArray();

            var res = await Task.WhenAll(t);
            return res.Sum();
        }

        static async Task<int> MakeRequest(string name)
        {

            using var hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:5275/");
            var res = await hc.GetStringAsync(name);
            //WriteLine(res);
            WriteLine("Task " + name + " succeeded");
            return res.Length;


        }
    }
}
