using NetCore2BlocklyNew;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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


app.Run();
