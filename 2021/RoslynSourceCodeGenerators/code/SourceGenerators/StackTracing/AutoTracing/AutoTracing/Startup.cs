using AT_BL;
using AT_DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTracing
{
    public class Startup
    {
//        public static readonly ActivitySource MyActivitySource = new ActivitySource(ThisAssembly.Project.AssemblyName);
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<PersonContext>();
            services.AddScoped<PersonRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoTracing", Version = "v1" });
            });
            services.AddDbContext<DatabaseContext>(opt => opt.UseInMemoryDatabase("db_Andrei"));

            services.AddOpenTelemetryTracing((builder) => builder
                    .SetResourceBuilder(

                            ResourceBuilder
                            .CreateDefault()
                            .AddService(ThisAssembly.Project.AssemblyName)
                            .AddService("test")
                            .AddTelemetrySdk()
                        )
                        .AddAspNetCoreInstrumentation()
                        //.AddEntityFrameworkInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddSqlClientInstrumentation(opt=> {
                            opt.EnableConnectionLevelAttributes = true;
                            opt.RecordException = true;
                            opt.SetDbStatementForText = true;
                            opt.SetDbStatementForStoredProcedure = true;
                            
                        })
                            .AddSource("OpenTelemetry.Instrumentation.AspNetCore")
                        .AddSource("asd")
                        .AddZipkinExporter(c =>
                        {
                            //docker run -d -p 9411:9411 openzipkin/zipkin
                            //http://localhost:9411/
                            c.Endpoint = new Uri("http://localhost:9411/api/v2/spans");
                        })

                    .AddConsoleExporter()) ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoTracing v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
