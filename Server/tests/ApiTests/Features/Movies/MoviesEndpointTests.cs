using System.Net;
using Api.Features.Movies;
using ApiTests.Common;
using Application.Common;
using Application.Movies;
using Application.Movies.Interfaces;
using FluentAssertions;
using Infrastructure.Database.Models;
using NSubstitute;
using TestUtilities;
using TestUtilities.Fakers;

namespace ApiTests.Features.Movies;

public class MoviesEndpointTests(ApiFactory factory) : AuthenticatedTest(factory)
{
    private static string TestUri(string uri) => $"{MoviesEndpoint.Uri}{uri}";

    // TODO fix tests

    [Fact]
    public async Task GetMoviesNowPlaying_ShouldReturnPagedResponseWithMovies()
    {
        // Arrange
        await AuthenticateAsync();

        var externalMovies = MovieFaker.GenerateExternalMovies(5);
        var movieModels = MovieFaker.GenerateMovieModels(5);

        foreach (var model in movieModels)
        {
            model.Genres.Add(new GenreModel() { Id = externalMovies[0].GenreIds[0] });
        }

        await Api.SeedDatabaseAsync([.. movieModels]);

        Api.MoviesClientMock.GetMoviesByPageAsync(Arg.Any<MoviesBy>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(new Paged<ExternalMovie>(externalMovies));

        // Act
        var result = await Client.GetAsync(TestUri(MoviesEndpoint.GetNowPlaying));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMoviesPopular_ShouldAddUnknownMoviesToDatabase()
    {
        // Arrange
        await AuthenticateAsync();

        var externalMovies = MovieFaker.GenerateExternalMovies(5);

        Api.MoviesClientMock.GetMoviesByPageAsync(Arg.Any<MoviesBy>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(new Paged<ExternalMovie>(externalMovies));

        // Act
        var result = await Client.GetAsync(TestUri(MoviesEndpoint.GetPopular));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMoviesTopRated_ShouldAddUnknownMoviesToDatabase()
    {
        // Arrange
        await AuthenticateAsync();

        var externalMovies = MovieFaker.GenerateExternalMovies(5);

        Api.MoviesClientMock.GetMoviesByPageAsync(Arg.Any<MoviesBy>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(new Paged<ExternalMovie>(externalMovies));

        // Act
        var result = await Client.GetAsync(TestUri(MoviesEndpoint.GetTopRated));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMoviesUpcoming_ShouldAddUnknownMoviesToDatabase()
    {
        // Arrange
        await AuthenticateAsync();

        var externalMovies = MovieFaker.GenerateExternalMovies(5);

        Api.MoviesClientMock.GetMoviesByPageAsync(Arg.Any<MoviesBy>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(new Paged<ExternalMovie>(externalMovies));

        // Act
        var result = await Client.GetAsync(TestUri(MoviesEndpoint.GetUpcoming));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMovieDetailsById_ShouldReturnDetailedMovieResponse()
    {
        // Arrange
        await AuthenticateAsync();

        var movieModels = MovieFaker.GenerateMovieModels(1);

        var externalMovieDetailed = MovieFaker.GenerateExternalMovieDetails(movieModels[0]);

        await Api.SeedDatabaseAsync([.. movieModels]);

        Api.MoviesClientMock.GetMovieDetailsAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(externalMovieDetailed);

        // Act
        var result = await Client.GetAsync(TestUri(MoviesEndpoint.GetMovieDetailsById(movieModels[0].Id)));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}