using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Orders.IntegrationTests;

public sealed class OrdersApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public OrdersApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Health_ReturnsSuccess()
    {
        using HttpResponseMessage response = await _client.GetAsync("/health");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_ReturnsCreatedAndLocation()
    {
        var request = new
        {
            customer = "Ada",
            lines = new[] { new { sku = "book", quantity = 2, unitPrice = 12.50m } }
        };

        using HttpResponseMessage response = await _client.PostAsJsonAsync("/orders", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);

        using HttpResponseMessage getResponse = await _client.GetAsync(response.Headers.Location);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_WithNoLines_ReturnsBadRequest()
    {
        using HttpResponseMessage response = await _client.PostAsJsonAsync("/orders", new
        {
            customer = "Ada",
            lines = Array.Empty<object>()
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
