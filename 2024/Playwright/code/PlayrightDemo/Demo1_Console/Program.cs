using Microsoft.Playwright;

Console.WriteLine("Demo Playwright!");

using var playwright = await Playwright.CreateAsync();
var request = await playwright.APIRequest.NewContextAsync();

// Make a GET request to the weather forecast API
var response = await request.GetAsync("http://localhost:5348/weatherforecast");

// Check if the response is successful
if (response.Ok)
{
    var responseBody = await response.TextAsync();
    Console.WriteLine("Weather Forecast API Response:");
    Console.WriteLine(responseBody);
}
else
{
    Console.WriteLine($"Failed to fetch weather forecast. Status: {response.Status}");
}