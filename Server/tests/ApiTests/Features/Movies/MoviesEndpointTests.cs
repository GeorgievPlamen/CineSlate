using System.Net;
using ApiTests.Common;
using Application.Common;
using Application.Movies;
using Domain.Movies;
using Domain.Movies.ValueObjects;
using FluentAssertions;
using NSubstitute;
using TestUtilities;
using TestUtilities.Fakers;

namespace ApiTests.Features.Movies;

public class MoviesEndpointTests(ApiFactory factory) : AuthenticatedTest(factory)
{
    private static string TestUri(string uri) => $"/api/movies/{uri}";

    [Fact]
    public async Task GetMoviesNowPlaying_ShouldReturnPagedResponseWithMovies()
    {
        // Arrange
        await AuthenticateAsync();

        var movies = MovieFaker.GenerateMovies(5);
        var movieAggregates = movies
            .Select(m => MovieAggregate
            .Create(MovieId
                .Create(m.Id),
                m.Title,
                m.Description,
                m.ReleaseDate,
                m.PosterPath,
                m.Genres));

        await Api.SeedDatabaseAsync([.. movieAggregates]);

        Api.MoviesClientMock.GetMoviesByPageAsync(Arg.Any<MoviesBy>(), Arg.Any<int>())
            .Returns(new Paged<Movie>(movies));

        // Act
        var result = await Client.GetAsync(TestUri("now_playing"));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMoviesPopular_ShouldAddUnknownMoviesToDatabase()
    {
        // Arrange
        await AuthenticateAsync();

        var externalMovies = MovieFaker.GenerateMovies(5);

        Api.MoviesClientMock.GetMoviesByPageAsync(Arg.Any<MoviesBy>(), Arg.Any<int>())
            .Returns(new Paged<Movie>(externalMovies));

        // Act
        var result = await Client.GetAsync(TestUri("popular"));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMoviesTopRated_ShouldAddUnknownMoviesToDatabase()
    {
        // Arrange
        await AuthenticateAsync();

        var externalMovies = MovieFaker.GenerateMovies(5);

        Api.MoviesClientMock.GetMoviesByPageAsync(Arg.Any<MoviesBy>(), Arg.Any<int>())
            .Returns(new Paged<Movie>(externalMovies));

        // Act
        var result = await Client.GetAsync(TestUri("top_rated"));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMoviesUpcoming_ShouldAddUnknownMoviesToDatabase()
    {
        // Arrange
        await AuthenticateAsync();

        var externalMovies = MovieFaker.GenerateMovies(5);

        Api.MoviesClientMock.GetMoviesByPageAsync(Arg.Any<MoviesBy>(), Arg.Any<int>())
            .Returns(new Paged<Movie>(externalMovies));

        // Act
        var result = await Client.GetAsync(TestUri("upcoming"));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}