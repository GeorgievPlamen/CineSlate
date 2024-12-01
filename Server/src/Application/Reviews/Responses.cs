namespace Application.Reviews;

public record ReviewResponse(
    int Rating,
    int MovieId,
    string Text,
    bool ContainsSpoilers);