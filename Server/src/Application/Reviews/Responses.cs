namespace Application.Reviews;

public record ReviewResponse(
    int Rating,
    string Text,
    int MovieId,
    string MovieTitle,
    DateOnly ReleaseDate,
    string PosterPath,
    Guid AuthorId,
    bool ContainsSpoilers);