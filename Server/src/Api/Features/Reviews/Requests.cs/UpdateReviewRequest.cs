namespace Api.Features.Reviews.Requests.cs;

public record UpdateReviewRequest(
    Guid ReviewId,
    int Rating,
    string? Text,
    bool ContainsSpoilers);