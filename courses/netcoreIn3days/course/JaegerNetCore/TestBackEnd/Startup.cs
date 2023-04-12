using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace.Configuration;
using OpenTracing;
using OpenTracing.Util;

namespace TestBackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddLogging();
            services.AddOpenTelemetry(b =>
            {


                b//.AddRequestAdapter()
                .UseJaeger(c =>
                {
                    var s = Configuration.GetSection("Jaeger");
                    
                    s.Bind(c);
                    
                    
                })
               ; 
                var x = new Dictionary<string, object>() {                
                { "PC", Environment.MachineName } };
                b.SetResource(new Resource(x.ToArray()));






            });



            //services.AddSingleton<ITracer>(serviceProvider =>
            //{
            //    var config = serviceProvider.GetRequiredService<IConfiguration>();
            //    var jaeger = config.GetSection("Jaeger");

            //    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            //    var configJaeger = Jaeger.Configuration.FromIConfiguration(loggerFactory, jaeger);
            //    var tracer = configJaeger.GetTracer();
            //    // Allows code that can't use DI to also access the tracer.
            //    GlobalTracer.Register(tracer);

            //    return tracer;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
