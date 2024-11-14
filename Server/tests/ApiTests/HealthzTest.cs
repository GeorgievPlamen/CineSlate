using System.Net.Http.Json;
using Api.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ApiTests;

public class HealthzTest : IClassFixture<WebApplicationFactory<IApiMarker>>
{
    private readonly HttpClient _httpClient;

    public HealthzTest(WebApplicationFactory<IApiMarker> webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task Root_ShouldReturnOk_WhenApiIsAlive()
    {
        // Act
        var response = await _httpClient.GetAsync("/");
        var content = await response.Content.ReadFromJsonAsync<string>();
    
        // Assert
        response.Should().NotBeNull();
        content.Should().Match("Hello there :)"); 
    }
}