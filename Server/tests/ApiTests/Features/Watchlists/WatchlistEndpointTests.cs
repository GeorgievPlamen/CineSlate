using System.Net;
using System.Net.Http.Json;

using Api.Features.Watchlist;

using ApiTests.Common;

using Domain.Movies.ValueObjects;

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
    public async Task GetWatchlist_ShouldReturn_Success_WhenValid()
    {
        // Arrange
        var userId = await AuthenticateAsync();
        var watchlistModels = WatchlistFaker.GenerateWatchlist(userId);

        await Api.SeedDatabaseAsync([watchlistModels]);

        // Act
        var result = await Client.GetAsync(TestUri($"/"));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task RemoveFromWatchlist_ShouldReturn_Success_WhenValid()
    {
        // Arrange
        var userId = await AuthenticateAsync();
        var watchlistModels = WatchlistFaker.GenerateWatchlist(userId);

        await Api.SeedDatabaseAsync([watchlistModels]);

        // Act
        var result = await Client.PostAsync(TestUri($"/remove?movieId={watchlistModels.MovieToWatchModels[0].MovieId}&watched={watchlistModels.MovieToWatchModels[0].Watched}"), null);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteWatchlist_ShouldReturn_Success_WhenValid()
    {
        // Arrange
        var userId = await AuthenticateAsync();
        var watchlistModels = WatchlistFaker.GenerateWatchlist(userId);

        await Api.SeedDatabaseAsync([watchlistModels]);

        // Act
        var result = await Client.DeleteAsync(TestUri($"/"));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task UpdateWatchedStatus_ShouldReturn_Success_WhenValid()
    {
        // Arrange
        var userId = await AuthenticateAsync();
        var watchlistModels = WatchlistFaker.GenerateWatchlist(userId);

        await Api.SeedDatabaseAsync([watchlistModels]);

        // Act
        var result = await Client.PutAsync(TestUri($"/{watchlistModels.MovieToWatchModels[0].MovieId}?watched={!watchlistModels.MovieToWatchModels[0].Watched}"), null);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}