var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


// Configure the HTTP request pipeline.
//app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/base64/encode/{plainText}", (string plainText) =>
{
    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
    return Results.Text(System.Convert.ToBase64String(plainTextBytes));
});

app.MapGet("/",() =>
{
    return Results.Text("See swagger");
});

app.MapGet("/base64/decode/{base64EncodedData}", (string base64EncodedData) =>
{

    var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
    return Results.Text(System.Text.Encoding.UTF8.GetString(base64EncodedBytes));
});

app.MapDefaultEndpoints();

app.Run();

