
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

