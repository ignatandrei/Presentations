using DalManual;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using ManualCode.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

//app.MapDepartmentEndpoints();

app.Run();


//public static class DepartmentEndpoints
//{
//	public static void MapDepartmentEndpoints (this IEndpointRouteBuilder routes)
//    {
//        var group = routes.MapGroup("/api/Department").WithTags(nameof(Department));

//        group.MapGet("/", async (TestsDatabase2RestContext db) =>
//        {
//            return await db.Department.ToListAsync();
//        })
//        .WithName("GetAllDepartments")
//        .WithOpenApi();

//        group.MapGet("/{id}", async Task<Results<Ok<Department>, NotFound>> (long iddepartment, TestsDatabase2RestContext db) =>
//        {
//            return await db.Department.AsNoTracking()
//                .FirstOrDefaultAsync(model => model.Iddepartment == iddepartment)
//                is Department model
//                    ? TypedResults.Ok(model)
//                    : TypedResults.NotFound();
//        })
//        .WithName("GetDepartmentById")
//        .WithOpenApi();

//        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (long iddepartment, Department department, TestsDatabase2RestContext db) =>
//        {
//            var affected = await db.Department
//                .Where(model => model.Iddepartment == iddepartment)
//                .ExecuteUpdateAsync(setters => setters
//                  .SetProperty(m => m.Iddepartment, department.Iddepartment)
//                  .SetProperty(m => m.Name, department.Name)
//                );

//            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
//        })
//        .WithName("UpdateDepartment")
//        .WithOpenApi();

//        group.MapPost("/", async (Department department, TestsDatabase2RestContext db) =>
//        {
//            db.Department.Add(department);
//            await db.SaveChangesAsync();
//            return TypedResults.Created($"/api/Department/{department.Iddepartment}",department);
//        })
//        .WithName("CreateDepartment")
//        .WithOpenApi();

//        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (long iddepartment, TestsDatabase2RestContext db) =>
//        {
//            var affected = await db.Department
//                .Where(model => model.Iddepartment == iddepartment)
//                .ExecuteDeleteAsync();

//            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
//        })
//        .WithName("DeleteDepartment")
//        .WithOpenApi();
//    }
//}