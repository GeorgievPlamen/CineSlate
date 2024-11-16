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
        response.Headers.TryGetValues("TraceId", out IEnumerable<string>? traceIds);
        var traceId = traceIds?.FirstOrDefault();

        // Assert
        traceIds.Should().NotBeNullOrEmpty();
        traceId.Should().NotBeNullOrEmpty();
    }
}