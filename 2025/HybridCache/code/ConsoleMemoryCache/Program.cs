using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

//builder.Services.AddTransient<IExampleTransientService, ExampleTransientService>();


using IHost host = builder.Build();
