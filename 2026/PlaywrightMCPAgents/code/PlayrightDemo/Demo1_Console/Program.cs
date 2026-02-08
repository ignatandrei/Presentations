using Microsoft.Playwright;
//https://playwright.dev/dotnet/docs/browsers
var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });
if (exitCode != 0)
{
    throw new Exception($"Playwright exited with code {exitCode}");
}
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