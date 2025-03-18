using System.Net;

using Api.Features.Movies;

using ApiTests.Common;

using Application.Common;
using Application.Movies;
using Application.Movies.Interfaces;

using FluentAssertions;

using NSubstitute;

using TestUtilities;
using TestUtilities.Fakers;

namespace ApiTests.Features.Movies;

public class MoviesEndpointTests(ApiFactory factory) : AuthenticatedTest(factory)
{
    private static string TestUri(string uri) => $"{MoviesEndpoint.Uri}{uri}";

    [Fact]
    public async Task GetMoviesNowPlaying_ShouldReturnPagedResponseWithMovies()
    {
        // Arrange
        await AuthenticateAsync();

        var externalMovies = MovieFaker.GenerateExternalMovies(5);
        var movieModels = MovieFaker.GenerateMovieModels(5);

        await Api.SeedDatabaseAsync(movieModels);

        Api.MoviesClientMock.GetMoviesByPageAsync(Arg.Any<MoviesBy>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(new Paged<ExternalMovie>(externalMovies));

        // Act
        var result = await Client.GetAsync(TestUri("/now_playing"));

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
        var result = await Client.GetAsync(TestUri("/popular"));

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
        var result = await Client.GetAsync(TestUri("/top_rated"));

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
        var result = await Client.GetAsync(TestUri("/upcoming"));

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

        await Api.SeedDatabaseAsync(movieModels);

        Api.MoviesClientMock.GetMovieDetailsAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(externalMovieDetailed);

        // Act
        var result = await Client.GetAsync(TestUri($"/{movieModels[0].Id}"));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMoviesGetMoviesByTitle_ShouldReturnPagedResponseWithMovies()
    {
        // Arrange
        await AuthenticateAsync();

        var externalMovies = MovieFaker.GenerateExternalMovies(5);
        var movieModels = MovieFaker.GenerateMovieModels(5);

        await Api.SeedDatabaseAsync(movieModels);

        Api.MoviesClientMock.GetMoviesByTitle(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(new Paged<ExternalMovie>(externalMovies));

        // Act
        var result = await Client.GetAsync(TestUri("/search?title=" + externalMovies[0].Title));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMoviesByFilters_ShouldReturnPagedResponseWithMovies()
    {
        // Arrange
        await AuthenticateAsync();

        var externalMovies = MovieFaker.GenerateExternalMovies(5);
        var movieModels = MovieFaker.GenerateMovieModels(5);

        await Api.SeedDatabaseAsync(movieModels);

        Api.MoviesClientMock.GetMoviesByGenreAndYear(Arg.Any<int>(), Arg.Any<int[]?>(), Arg.Any<int?>(), Arg.Any<CancellationToken>())
            .Returns(new Paged<ExternalMovie>(externalMovies));

        // Act
        var result = await Client.GetAsync(TestUri("/filter?genreIds=" + externalMovies[0].GenreIds[0]));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}