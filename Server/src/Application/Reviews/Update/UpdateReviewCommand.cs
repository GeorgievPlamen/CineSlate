using Application.Common;
using Domain.Movies.Reviews.ValueObjects;
using MediatR;

namespace Application.Reviews.Update;

public record UpdateReviewCommand(
    Guid ReviewId,
    int Rating,
    string? Text,
    bool ContainsSpoilers) : IRequest<Result<ReviewId>>;