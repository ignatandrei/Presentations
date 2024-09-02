
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SqlConnection>(_ 
        => new(builder.Configuration.GetConnectionString("AddressBook")))
    ;
//TODO: uncomment the following line for ASPIRE
//builder.AddSqlServerClient("AddressBook");

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();
app.MapGet("/", () => "Please see /swagger");
app.MapGet("/addressbook", async ([FromServices]SqlConnection db) =>
{
    const string sql = """
                SELECT Id, FirstName, LastName, Email, Phone
                FROM Contacts
                """;

    return await db.QueryAsync<Contact>(sql);
})
    .WithName("SeeBook")
    .WithOpenApi();
;

app.MapGet("/addressbook/{id}", async ([FromRoute] int id, [FromServices] SqlConnection db) =>
{
    const string sql = """
                SELECT Id, FirstName, LastName, Email, Phone
                FROM Contacts
                WHERE Id = @id
                """;

    return await db.QueryFirstOrDefaultAsync<Contact>(sql, new { id }) is { } contact
        ? Results.Ok(contact)
        : Results.NotFound();
});
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
