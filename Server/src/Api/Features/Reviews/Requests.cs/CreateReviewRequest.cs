namespace Api.Features.Reviews.Requests.cs;

public record CreateReviewRequest(
    int Rating,
    int MovieId,
    string Text,
    bool ContainsSpoilers);