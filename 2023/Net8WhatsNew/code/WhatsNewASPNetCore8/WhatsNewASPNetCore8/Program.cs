using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WhatsNewASPNetCore8;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKeyedSingleton<ICache, BigCache>("big");
builder.Services.AddKeyedSingleton<ICache, SmallCache>("small");
builder.Services.AddOptions<MyAppOptions>()
    .Bind(builder.Configuration.GetSection(MyAppOptions.ConfigName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!").WithTags("Weather");

app.MapGroup("v1").MapCacheEndpoints().WithTags("Caching");

app.RegisterWeatherEndpoints();

app.MapGet("/MyAppOptionsEndpoint", (IOptionsMonitor<MyAppOptions> opt) => opt.CurrentValue.AppDisplayName);

app.Run();
