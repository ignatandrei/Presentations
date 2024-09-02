using System.Diagnostics;

namespace ToDoTest;

[TestClass]
public class TestData
{
    [TestMethod]
    public async Task TestDataFromWebAPI()
    {
        // Arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.TodoWebAPI_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });
        await using var app = await appHost.BuildAsync();
        var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
        await app.StartAsync();

        // Act
        var httpClient = app.CreateHttpClient("todo");
        await resourceNotificationService.WaitForResourceAsync("todo", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));
        var response = await httpClient.GetAsync("/");


        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        // Act

        //https://github.com/davidfowl/WaitForDependenciesAspire
        await Task.Delay(10_000);        
        response = await httpClient.GetAsync("/addressbook");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var str= await response.Content.ReadAsStringAsync();
        Assert.IsTrue(str.Contains("Andrei"));
    }
}
