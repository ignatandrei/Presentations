using DBData;
using DBData.genContext;
using DBData.genDBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using MultiCacheDemo;
using OpenAPISwaggerUI;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddHybridCache();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                          .AllowAnyHeader()
                          .AllowCredentials()
                          .AllowAnyMethod()
                          .SetIsOriginAllowed(it=>true)
                          ;
                      });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMemoryCache();
builder.Services.AddTransient<SimpleRepo>();
builder.Services.AddTransient<CacheStatic>();
builder.Services.AddTransient<CacheIMemory>();
builder.Services.AddTransient<CacheIDistributed>();
builder.Services.AddTransient<CacheHybrid>();
var conStringData = builder.Configuration.GetConnectionString("EmpDep");

builder.Services.AddDbContext<EmpDepContext>(options =>
    options.UseSqlServer(conStringData));


builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString(
        "CachingData");
    options.SchemaName = "dbo";
    options.TableName = "TestCache";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseCors(MyAllowSpecificOrigins);
//if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenAPISwaggerUI();

}

#region static
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
#endregion

#region memory
app.MapGet("/imemory/employees", async ([FromServices] CacheIMemory cache) =>
{

    return await cache.EmployeeAsDisplay();
});
app.MapGet("/imemory/departments", async ([FromServices] CacheIMemory cache) =>
{

    return await cache.Departments();
});

app.MapPost("/imemory/departments", async ([FromServices] CacheIMemory cache, [FromBody] DepartmentTable departmentTable) =>
{
    var ret = await cache.UpdateDepartmentName(departmentTable);
    return Results.Ok(ret);
});
#endregion

#region distributed
app.MapGet("/distributed/employees", async ([FromServices] CacheIDistributed cache) =>
{

    return await cache.EmployeeAsDisplay();
});
app.MapGet("/distributed/departments", async ([FromServices] CacheIDistributed cache) =>
{

    return await cache.Departments();
});

app.MapPost("/distributed/departments", async ([FromServices] CacheIDistributed cache, [FromBody] DepartmentTable departmentTable) =>
{
    var ret = await cache.UpdateDepartmentName(departmentTable);
    return Results.Ok(ret);
});
#endregion

#region hybrid
app.MapGet("/hybrid/employees", async ([FromServices] CacheHybrid cache) =>
{

    return await cache.EmployeeAsDisplay();
});
app.MapGet("/hybrid/departments", async ([FromServices] CacheHybrid cache) =>
{

    return await cache.Departments();
});

app.MapPost("/hybrid/departments", async ([FromServices] CacheHybrid cache, [FromBody] DepartmentTable departmentTable) =>
{
    var ret = await cache.UpdateDepartmentName(departmentTable);
    return Results.Ok(ret);
});
#endregion


app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
