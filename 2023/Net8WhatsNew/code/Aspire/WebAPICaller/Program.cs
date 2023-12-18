var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddCommandLine(args);
builder.AddServiceDefaults();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine(builder.Configuration.GetDebugView());
var url= Environment.GetEnvironmentVariable("services__weatherapi__1");
url ??= "http://localhost:5000";
var h =new HttpClient();
h.BaseAddress = new Uri(url);
builder.Services.AddKeyedSingleton("AspNetWeather",h);

url = Environment.GetEnvironmentVariable("NODE_URL");
url ??= "http://localhost:5000";
var hNode = new HttpClient();
hNode.BaseAddress = new Uri(url);
builder.Services.AddKeyedSingleton("NodeWeather", hNode);


builder.Services.AddKeyedSingleton("HttpWeather", h);


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
