using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Exporter.Jaeger;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Trace.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitProducer
{
    class Program
    {
        static private Activity GetNewActionFromCurrent(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            //very important! to not imbricate own actions
            Activity.Current = null;
            var activity = new Activity(memberName)
                .Start();


            activity.AddTag("CallerMemberName", memberName);
            activity.AddTag("CallerFilePath", sourceFilePath);
            activity.AddTag("CallerLineNumber", sourceLineNumber.ToString());


            return activity;


        }
        static Tracer tracer;
        static void Main(string[] args)
        {

            var opt = new JaegerExporterOptions();
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var serviceProvider = new ServiceCollection()
                    .AddLogging()
                    .AddSingleton<IConfiguration>(config)
                    .AddOpenTelemetry(b =>
                    {
                        b//.AddRequestAdapter()
                        .UseJaeger(c =>
                        {
                            var s = config.GetSection("Jaeger");

                            s.Bind(c);


                        });
                        var x = new Dictionary<string, object>() {
                            { "PC", Environment.MachineName } };
                        b.SetResource(new Resource(x.ToArray()));

                    }).BuildServiceProvider();
            var f = serviceProvider.GetRequiredService<TracerFactoryBase>();
            tracer = f.GetTracer("custom");






            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password"
            };


            while (true)
            {
                using (var connection = factory.CreateConnection())
                {
                    using (var model = connection.CreateModel())
                    {
                        model.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                        string message = "Message sent at :" + DateTime.Now.ToString("o");
                        var body = Encoding.UTF8.GetBytes(message);
                        var props = model.CreateBasicProperties();
                        var act = GetNewActionFromCurrent();
                        props.Headers = new Dictionary<string, object>();
                        //props.ContentType= "text/plain";
                        //props.DeliveryMode = 1;//persistent
                        props.Persistent = true;
                        props.Expiration = "36000000";
                        //props.ContentEncoding = "UTF-8";
                        props.CorrelationId = act.TraceId.ToHexString();
                        props.MessageId = act.SpanId.ToHexString();


                        TelemetrySpan tsMultiple;
                        using (var span = tracer.StartActiveSpanFromActivity(act.OperationName, act, SpanKind.Client, out tsMultiple))
                        {
                            tsMultiple.SetAttribute("LoggingTrace", props.CorrelationId);
                            tsMultiple.SetAttribute("LoggingSpan", props.MessageId);
                            Console.WriteLine("Trace : " + props.CorrelationId);
                            Console.WriteLine("Span : " + props.MessageId);
                            model.BasicPublish(exchange: "",
                                             routingKey: "hello",
                                             basicProperties: props,
                                             body: body);
                            Console.WriteLine("FIRST message Sent {0}", message);


                        }
                    }
                }
                Console.ReadLine();
            }
        }

    }
}
