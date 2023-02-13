

// for logging
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// for logging
builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//for health of the service
builder.Services
    .AddHealthChecks()
    .AddCheck<RandomHealthCheck>("YouCanHaveInClass")
    .AddUrlGroup(new Uri("http://httpbin.org/status/200"))
    //.AddApplicationStatus()
    ;
builder.Services
    .AddHealthChecksUI(opt =>
    {
        opt.AddHealthCheckEndpoint("default api", "/healthz");
    })
    .AddInMemoryStorage();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();

//app.UseAuthorization();

//shutdown
app.Use(async (context, next) =>
{
    if(!UtilsController.IsCancellationRequired)
        await next(context);
    else
        context.Response.StatusCode= 418;
});
app.MapControllers();
app.UseBlocklyAutomation();
app.UseBlocklyUI(app.Environment);

//for health of the service
app.MapHealthChecks("healthz", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(opt =>
{ });

try
{
    //shutdown
    await app.RunAsync(UtilsController.cts.Token);
}
finally
{
    //for logging
    NLog.LogManager.Shutdown();
}