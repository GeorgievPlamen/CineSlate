using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using TestUtilities;

namespace ApiTests;

public class HealthzTest(ApiFactory api) : IClassFixture<ApiFactory>
{
    private readonly HttpClient _httpClient = api.CreateClient();

    [Fact]
    public async Task Root_ShouldReturnOk_WhenApiIsAlive()
    {
        // Act
        var response = await _httpClient.GetAsync("/");
        var content = await response.Content.ReadFromJsonAsync<string>();
    
        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().Match("Hello there :)"); 
    }

    [Fact]
    public async Task Root_ShouldReturnOkWithTraceId_WhenApiIsAlive()
    {
        // Act
        var response = await _httpClient.GetAsync("/");
        response.Headers.TryGetValues("Set-Cookie",out IEnumerable<string>? cookies);
        var traceId = cookies?.FirstOrDefault(x => x.Contains("TraceId"));
    
        // Assert
        cookies.Should().NotBeNullOrEmpty();
        traceId.Should().NotBeNullOrEmpty();
    }
}