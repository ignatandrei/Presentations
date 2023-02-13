
using System;

namespace TestData;

public class TestWebAPI:IClassFixture<WebApplicationFactory<Program>>
{

    private readonly WebApplicationFactory<Program> _factory;

    public TestWebAPI(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    [Theory]
    [InlineData("/Utils/Test")]
    public async Task TestUtilsController(string url)
    {
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        var cnt = await response.Content.ReadAsStringAsync();
        Assert.Equal("10",cnt);
    }
}