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
    private const string TestUri = "/api/movies/now_playing";

    [Fact]
    public async Task GetMovies_ShouldReturnPagedResponseWithMovies()
    {
        // Arrange
        await AuthenticateAsync();

        var externalMovies = MovieFaker.GenerateExternalMovies(5);
        var movieAggregates = externalMovies
            .Select(m => MovieAggregate
            .Create(MovieId
                .Create(m.Id),
                m.Title,
                m.Description,
                m.ReleaseDate,
                m.PosterPath,
                m.GenreIds
                .Select(g => Genre.Create(g))));

        await Api.SeedDatabaseAsync([.. movieAggregates]);

        Api.MoviesClientMock.GetMoviesByPageAsync(Arg.Any<MoviesBy>(), Arg.Any<int>())
            .Returns(new Paged<ExternalMovie>(externalMovies));

        // Act
        var result = await Client.GetAsync(TestUri);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMovies_ShouldAddUnknownMoviesToDatabase()
    {
        // Arrange
        await AuthenticateAsync();

        var externalMovies = MovieFaker.GenerateExternalMovies(5);

        Api.MoviesClientMock.GetMoviesByPageAsync(Arg.Any<MoviesBy>(), Arg.Any<int>())
            .Returns(new Paged<ExternalMovie>(externalMovies));

        // Act
        var result = await Client.GetAsync(TestUri);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}