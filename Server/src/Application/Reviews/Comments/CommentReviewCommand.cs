using Application.Common;

using Domain.Movies.Reviews.ValueObjects;

using MediatR;

namespace Application.Reviews.Comments;

public record CommentReviewCommand(ReviewId ReviewId, string Comment) : IRequest<Result<Unit>>;