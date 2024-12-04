using System.Net;
using System.Net.Http.Json;
using Api.Features.Reviews;
using Api.Features.Reviews.Requests.cs;
using ApiTests.Common;
using FluentAssertions;
using TestUtilities;
using TestUtilities.Fakers;

namespace ApiTests.Features.Reviews;

public class ReviewsEndpointTests(ApiFactory factory) : AuthenticatedTest(factory)
{
    private static string TestUri(string uri) => $"{ReviewsEndpoint.Uri}{uri}";

    [Fact]
    public async Task CreateReview_ShouldReturn_IdForNewReview_WhenSuccessfull()
    {
        // Arrange
        var movieModels = MovieFaker.GenerateMovieModels(1);
        var request = new CreateReviewRequest(4, movieModels[0].Id, "Movie was great!", false);

        await AuthenticateAsync();
        await Api.SeedDatabaseAsync(movieModels);

        // Act
        var result = await Client.PostAsJsonAsync(TestUri(ReviewsEndpoint.Create), request);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task GetReviews_ShouldReturn_ListofReviews()
    {
        // Arrange
        var movieModels = MovieFaker.GenerateMovieModels(1);
        var reviews = ReviewFaker.GenerateReviews(5);
        movieModels[0].Reviews = reviews;

        await AuthenticateAsync();
        await Api.SeedDatabaseAsync(movieModels);

        // Act
        var result = await Client.GetAsync(TestUri(ReviewsEndpoint.Get + "?page=1"));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}