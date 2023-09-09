using CachingNetObj;
using NetCore2BlocklyNew;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<clsCachedData>();
builder.Services.AddMemoryCache();
var app = builder.Build();
app.UseBlocklyUI(app.Environment);
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(ct =>
    {
        ct.DocumentTitle = "Use Blockly Automation!";
        ct.HeadContent = "Goto <h1><a href='/blocklyAutomation'>VisualAPI</a></h1> and see <h1>Example</h1>";
    });
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();
app.UseBlocklyAutomation();

app.Run();
