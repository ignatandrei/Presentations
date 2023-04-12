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


namespace RabbitConsumer
{
    class Program
    {
        static private Activity GetNewActionFromCurrent(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            //var curent = Activity.Current;
            
            var activity = new Activity(memberName);
            

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
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += Consumer_Received;


                    channel.BasicConsume(queue: "hello",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine("[Consumer] Press [enter] to exit.");
                    Console.ReadLine();

                }
            }
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs ea)
        {

            var body = ea.Body.ToArray();

            var act = GetNewActionFromCurrent();
            var props = ea.BasicProperties;
            if (props != null)
            {
                Console.WriteLine("Received Trace : " + props.CorrelationId + "-!");
                Console.WriteLine("Received Span : " + props.MessageId + "-!");
                var traceidHex = props.CorrelationId;
                var spanIdHex = props.MessageId;
                var traceId = ActivityTraceId.CreateFromString(traceidHex);
                var spanId = ActivitySpanId.CreateFromString(spanIdHex);
                act.SetParentId(traceId, spanId);
            }
            act.Start();
            TelemetrySpan tsMultiple;
            using (var span = tracer.StartActiveSpanFromActivity(act.OperationName, act, SpanKind.Client, out tsMultiple))
            {
                //tsMultiple.SetAttribute("LoggingTrace", act.TraceId.ToHexString());
                //tsMultiple.SetAttribute("LoggingSpan", act.SpanId.ToHexString());

                //Console.WriteLine(f.Key + f.Value);
                var message = Encoding.UTF8.GetString(body);
                tsMultiple.SetAttribute("message received", message);
                DoImportantJob(message);
                Console.WriteLine(" [x] Received {0}", message);
            }
        }

        private static void DoImportantJob(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password"
            };
            Thread.Sleep(2 * 1000);
            var act = GetNewActionFromCurrent();
            act.Start();
            TelemetrySpan tsMultiple;
            using (var span = tracer.StartActiveSpanFromActivity(act.OperationName, act, SpanKind.Client, out tsMultiple))
            {
                //send back
                using (var connection = factory.CreateConnection())
                {
                    using (var model = connection.CreateModel())
                    {
                        model.QueueDeclare(queue: "BackSendQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                        string Newmessage = "Reply :" + DateTime.Now.ToString("o");
                        var body = Encoding.UTF8.GetBytes(message);
                        var props = model.CreateBasicProperties();
                        props.Headers = new Dictionary<string, object>();
                        //props.ContentType= "text/plain";
                        //props.DeliveryMode = 1;//persistent
                        props.Persistent = true;
                        props.Expiration = "36000000";
                        //props.ContentEncoding = "UTF-8";
                        props.CorrelationId = act.TraceId.ToHexString();
                        props.MessageId = act.SpanId.ToHexString();


                        tsMultiple.SetAttribute("LoggingTrace", props.CorrelationId);
                        tsMultiple.SetAttribute("LoggingSpan", props.MessageId);
                        Console.WriteLine("Sent Trace : " + props.CorrelationId);
                        Console.WriteLine("Sent Span : " + props.MessageId);
                        model.BasicPublish(exchange: "",
                                         routingKey: "BackSendQueue",
                                         basicProperties: props,
                                         body: body);
                        Console.WriteLine(" [BackSendQueue] Sent {0}", message);
                        act.Stop();

                    }
                }
            }
        }
    }
}
