using DalManual;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);


var modelBuilder = new ODataConventionModelBuilder();

modelBuilder.EntitySet<Department>("Department").EntityType.HasKey(d=>d.Iddepartment);

// Add services to the container.
//https://learn.microsoft.com/en-us/odata/webapi-8/overview
builder.Services.AddControllers().AddOData(
    options => options
    .Select().Filter().OrderBy().Expand().Count()
    .EnableQueryFeatures()
    .AddRouteComponents(
        routePrefix: "odata",
        model: modelBuilder.GetEdmModel()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TestsDatabase2RestContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();
app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
