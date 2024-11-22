using System.Net;
using System.Text;
using System.Text.Json;
using Application.Movies;
using FluentAssertions;
using Infrastructure.Common.Models;
using Infrastructure.MoviesClient;
using InfrastructureTests.Common;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace InfrastructureTests.MoviesClient;

public class TMDBClientTests
{
    private readonly TestableHttpMessageHandler _httpMessageHandler = new();

    [Fact]
    public async Task Client_ReturnsEmptyList_WhenResponseIsNull()
    {
        // Arrange
        var responseContent = JsonSerializer.Serialize(new
        {
            page = 1,
            total_results = 2
        });

        _httpMessageHandler.SendAsyncFunc = _ => Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
        });

        var sut = CreateSut(_httpMessageHandler);

        // Act
        var result = await sut.GetMoviesByPageAsync(MoviesBy.now_playing, 1);

        // Assert
        result.Should().NotBeNull();
        result.Values.Should().BeEmpty();
    }

    [Fact]
    public async Task Client_ReturnsList_WhenResponseIsValid()
    {
        // Arrange
        var responseContent = JsonSerializer.Serialize(new
        {
            page = 1,
            results = new[]
            {
                new { id = 1, title = "Movie 1", overview = "Overview 1", release_date = "2024-01-01", poster_path = "poster1.jpg", genre_ids = new[] { 1, 2 } },
                new { id = 2, title = "Movie 2", overview = "Overview 2", release_date = "2024-01-02", poster_path = "poster2.jpg", genre_ids = new[] { 3, 4 } }
            },
            total_results = 2
        });

        _httpMessageHandler.SendAsyncFunc = _ => Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
        });

        var sut = CreateSut(_httpMessageHandler);

        // Act
        var result = await sut.GetMoviesByPageAsync(MoviesBy.now_playing, 1);

        // Assert
        result.Should().NotBeNull();
        result.Values.Should().NotBeEmpty();
    }

    private static TMDBClient CreateSut(HttpMessageHandler messageHandler)
    {
        var client = new HttpClient(messageHandler)
        { BaseAddress = new Uri("https://api.themoviedb.org/") };

        var factory = Substitute.For<IHttpClientFactory>();
        factory.CreateClient().Returns(client);

        var apiKeysOptions = Substitute.For<IOptions<ApiKeys>>();
        apiKeysOptions.Value.Returns(new ApiKeys { TMDBKey = "test-key" });

        return new TMDBClient(factory, apiKeysOptions);
    }
}