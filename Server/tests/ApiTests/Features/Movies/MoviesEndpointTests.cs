using System.Net;
using Api.Features.Movies;
using ApiTests.Common;
using Application.Common;
using Application.Movies;
using Application.Movies.Interfaces;
using Domain.Movies;
using Domain.Movies.ValueObjects;
using FluentAssertions;
using Infrastructure.Database.Models;
using Infrastructure.Repositories.MappingExtensions;
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

        var movies = MovieFaker.GenerateMovies(5);
        var externalMovies = MovieFaker.GenerateExternalMovies(5);
        var movieModels = movies
            .Select(m => MovieAggregate
            .Create(MovieId
                .Create(m.Id),
                m.Title,
                m.Description,
                m.ReleaseDate,
                m.PosterPath,
                m.Genres).ToModel([.. m.Genres.Select(g => new GenreModel() { Id = g.Id, Name = g.Value })]));

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

        var movies = MovieFaker.GenerateMovies(1);
        var movieModels = movies
            .Select(m => MovieAggregate
            .Create(MovieId
                .Create(m.Id),
                m.Title,
                m.Description,
                m.ReleaseDate,
                m.PosterPath,
                m.Genres).ToModel([.. m.Genres.Select(g => new GenreModel() { Id = g.Id, Name = g.Value })]));

        var externalMovieDetailed = MovieFaker.GenerateExternalMovieDetails(movies[0]);

        await Api.SeedDatabaseAsync([.. movieModels]);

        Api.MoviesClientMock.GetMovieDetailsAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(externalMovieDetailed);

        // Act
        var result = await Client.GetAsync(TestUri(MoviesEndpoint.GetMovieDetailsById(movies[0].Id)));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}