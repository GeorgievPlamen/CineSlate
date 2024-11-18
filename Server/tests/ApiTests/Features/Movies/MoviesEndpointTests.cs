using System.Net;
using ApiTests.Common;
using Application.Common;
using Application.Movies;
using FluentAssertions;
using NSubstitute;
using TestUtilities;
using TestUtilities.Fakers;

namespace ApiTests.Features.Movies;

public class MoviesEndpointTests(ApiFactory factory) : AuthenticatedTest(factory)
{

    [Fact]
    public async Task GetPopular_ShouldReturnPagedResponseWithMovies()
    {
        // Arrange
        await AuthenticateAsync();

        Api.MoviesClientMock.GetPopularMoviesByPageAsync(Arg.Any<int>())
            .Returns(new Paged<ExternalMovie>(MovieFaker.GenerateExternalMovies(5)));

        // Act
        var result = await Client.GetAsync("/api/movies/popular");

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}