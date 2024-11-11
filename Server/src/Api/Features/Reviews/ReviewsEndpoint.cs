namespace Api.Features.Reviews;

public static class ReviewsEndpoint
{
    public static void MapReviews(this WebApplication app)
    {
        var reviews = app.MapGroup("api/reviews").RequireAuthorization();

        reviews.MapGet("/", () => TypedResults.Ok("reviews"));
    }
}