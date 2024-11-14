using System.Net.Http.Json;
using System.Text.Json.Nodes;
using FluentAssertions;

namespace ApiTests;

public class HealthTest
{
    [Fact]
    public async Task Healthz_Returns_Ok()
    {
        // Arrange
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:8080")
        };
    
        // Act
        var response = await httpClient.GetAsync("/");

        var content = response.Content;
        var result = await content.ReadFromJsonAsync<string>();
    
        // Assert
        response.Should().NotBeNull();
        result.Should().Match("Hello there :)"); 
    }
}