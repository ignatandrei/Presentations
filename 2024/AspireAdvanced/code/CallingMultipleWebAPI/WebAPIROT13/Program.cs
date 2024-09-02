var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () =>
{
    return Results.Text("See swagger");
});

app.MapGet("/Rot13/{data}", (string data) =>
{
    return Results.Text(Transform(data));
});


app.Run();


static string Transform(string value)
{
    char[] array = value.ToCharArray();
    for (int i = 0; i < array.Length; i++)
    {
        int number = (int)array[i];

        if (number >= 'a' && number <= 'z')
        {
            if (number > 'm')
            {
                number -= 13;
            }
            else
            {
                number += 13;
            }
        }
        else if (number >= 'A' && number <= 'Z')
        {
            if (number > 'M')
            {
                number -= 13;
            }
            else
            {
                number += 13;
            }
        }
        array[i] = (char)number;
    }
    return new string(array);
}

