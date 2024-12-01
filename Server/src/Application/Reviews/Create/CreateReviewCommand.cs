using Application.Common;
using Domain.Movies.Reviews.ValueObjects;
using MediatR;

namespace Application.Reviews.Create;

public record CreateReviewCommand(
    int Rating,
    int MovieId,
    string Text,
    bool ContainsSpoilers) : IRequest<Result<ReviewId>>;