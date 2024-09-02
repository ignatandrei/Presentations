using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Text.RegularExpressions;

namespace AspireJavaScriptTests;

[TestClass]
public class E2ETest: PageTest
{
    [TestMethod]
    public async Task SimpleReadText()
    {
        //await Task.Delay(1000);
        //return;
        // Arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.AspireJavaScript_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });
        await using var app = await appHost.BuildAsync();
        var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
        await app.StartAsync();

        // Act
        var httpClient = app.CreateHttpClient("angular");
        await resourceNotificationService.WaitForResourceAsync("angular", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));
        var response = await httpClient.GetAsync("/");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(content.Contains("app-root"));
        Assert.IsTrue(content.Contains("Weather"));

    }
    //pwsh bin/Debug/net8.0/playwright.ps1 install
    //could be done in code , but this is not a playwright demo
    [TestMethod]
    public async Task RetrieveData()
    {
        // Arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.AspireJavaScript_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });
        await using var app = await appHost.BuildAsync();
        var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
        await app.StartAsync();

        // Act
        var httpClient = app.CreateHttpClient("angular");
        var url = httpClient.BaseAddress?.ToString();
        Assert.IsNotNull(url);
        await Page.GotoAsync(url);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Weather"));
        await Page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshot.png" });
    }
}
