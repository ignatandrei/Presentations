using DBData;
using DBData.genContext;
using DBData.genDBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiCacheDemo;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMemoryCache();
builder.Services.AddTransient<SimpleRepo>();
builder.Services.AddTransient<CacheIMemory>();
builder.Services.AddTransient<CacheStatic>();

var conStringData = builder.Configuration.GetConnectionString("EmpDep");

builder.Services.AddDbContext<EmpDepContext>(options =>
    options.UseSqlServer(conStringData));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

//if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapGet("/static/employees", async ([FromServices] CacheStatic cache) =>
{

    return await cache.EmployeeAsDisplay();
});
app.MapGet("/static/departments", async ([FromServices] CacheStatic cache) =>
{

    return await cache.Departments();
});

app.MapPost("/static/departments", async ([FromServices] CacheStatic cache, [FromBody] DepartmentTable departmentTable) =>
{
    var ret = await cache.UpdateDepartmentName(departmentTable);
    return Results.Ok(ret);
});


app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
