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

    [Fact]
    public async Task GetReviewsByMovieId_ShouldReturn_ListofReviews()
    {
        // Arrange
        var movieModels = MovieFaker.GenerateMovieModels(1);
        var reviews = ReviewFaker.GenerateReviews(5);
        movieModels[0].Reviews = reviews;

        await AuthenticateAsync();
        await Api.SeedDatabaseAsync(movieModels);

        // Act
        var result = await Client.GetAsync(TestUri($"/{movieModels[0].Id}" + "?page=1"));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetReviewDetailsById_ShouldReturn_200Ok()
    {
        // Arrange
        var movieModels = MovieFaker.GenerateMovieModels(1);
        var reviews = ReviewFaker.GenerateReviews(5);
        movieModels[0].Reviews = reviews;

        await AuthenticateAsync();
        await Api.SeedDatabaseAsync(movieModels);

        // Act
        var result = await Client.GetAsync(TestUri($"/details/{reviews[0].Id}"));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostLikeReview_ShouldReturn_200Ok()
    {
        // Arrange
        var movieModels = MovieFaker.GenerateMovieModels(1);
        var reviews = ReviewFaker.GenerateReviews(5);
        movieModels[0].Reviews = reviews;

        await AuthenticateAsync();
        await Api.SeedDatabaseAsync(movieModels);

        // Act
        var result = await Client.PostAsync(TestUri($"/like/{reviews[0]}"), null);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task CommentLikeReview_ShouldReturn_200Ok()
    {
        // Arrange
        var movieModels = MovieFaker.GenerateMovieModels(1);
        var reviews = ReviewFaker.GenerateReviews(5);
        movieModels[0].Reviews = reviews;

        await AuthenticateAsync();
        await Api.SeedDatabaseAsync(movieModels);

        var comment = JsonContent.Create("This is a fake comment");

        // Act
        var result = await Client.PostAsync(TestUri($"/comment/{reviews[0]}"), comment);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }


    [Fact]
    public async Task GetOwnReviews_ShouldReturn_ListofOwnReviews()
    {
        // Arrange
        var movieModels = MovieFaker.GenerateMovieModels(1);
        var reviews = ReviewFaker.GenerateReviews(5);
        movieModels[0].Reviews = reviews;

        await AuthenticateAsync();
        await Api.SeedDatabaseAsync(movieModels);

        // Act
        var result = await Client.GetAsync(TestUri($"/own/{movieModels[0].Id}"));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }


    [Fact]
    public async Task GetReviewsByUserId_ShouldReturn_ListofUserReviews()
    {
        // Arrange
        var movieModels = MovieFaker.GenerateMovieModels(1);
        var reviews = ReviewFaker.GenerateReviews(5);
        movieModels[0].Reviews = reviews;

        await AuthenticateAsync();
        await Api.SeedDatabaseAsync(movieModels);

        // Act
        var result = await Client.GetAsync(TestUri($"/user/{reviews[0].AuthorId}"));

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }


    [Fact]
    public async Task UpdareReview_ShouldReturn_ListofUserReviews()
    {
        // Arrange
        var movieModels = MovieFaker.GenerateMovieModels(1);
        var reviews = ReviewFaker.GenerateReviews(5);
        movieModels[0].Reviews = reviews;

        var request = new UpdateReviewRequest(reviews[0].Id, 3, "Fake review text", false);

        await AuthenticateAsync();
        await Api.SeedDatabaseAsync(movieModels);

        // Act
        var result = await Client.PutAsJsonAsync(TestUri($"/"), request);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}