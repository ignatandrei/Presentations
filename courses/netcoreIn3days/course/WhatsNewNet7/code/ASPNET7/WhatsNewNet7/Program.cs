
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 20;
        options.Window = TimeSpan.FromSeconds(5);
        //options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 20;
    }));
var app = builder.Build();
app.UseRateLimiter();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers().RequireRateLimiting("fixed");

app.UseBlocklyUI(app.Environment);
app.UseBlocklyAutomation();

app.Run();
