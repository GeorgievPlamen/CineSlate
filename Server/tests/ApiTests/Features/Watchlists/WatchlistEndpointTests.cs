using System.Net;

using Api.Features.Watchlist;

using ApiTests.Common;

using FluentAssertions;

using TestUtilities;
using TestUtilities.Fakers;

namespace ApiTests.Features.Watchlists;

public class WatchlistEndpointTests(ApiFactory factory) : AuthenticatedTest(factory)
{
    private static string TestUri(string uri) => $"{WatchlistEndpoint.Uri}{uri}";


    [Fact]
    public async Task AddToWatchlist_ShouldReturn_Success_WhenValid()
    {
        // Arrange
        var movieModels = MovieFaker.GenerateMovieModels(1);

        await AuthenticateAsync();
        await Api.SeedDatabaseAsync(movieModels);

        // Act
        var result = await Client.PostAsync(TestUri($"/{movieModels[0].Id}"), null);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetWatchlist_ShouldReturn_Success_WhenValid() // TODO
    {
        // Arrange
        var movieModels = MovieFaker.GenerateMovieModels(1);

        await AuthenticateAsync();
        await Api.SeedDatabaseAsync(movieModels);

        // Act
        var result = await Client.PostAsync(TestUri($"/{movieModels[0].Id}"), null);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task RemoveFromWatchlist_ShouldReturn_Success_WhenValid() // TODO
    {
        // Arrange
        var movieModels = MovieFaker.GenerateMovieModels(1);

        await AuthenticateAsync();
        await Api.SeedDatabaseAsync(movieModels);

        // Act
        var result = await Client.PostAsync(TestUri($"/{movieModels[0].Id}"), null);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}