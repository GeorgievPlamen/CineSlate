
using Application.Common;
using Application.Common.Interfaces;
using Application.Reviews.Interfaces;
using Application.Users.Interfaces;

using Domain.Movies.Reviews.Errors;
using Domain.Movies.Reviews.ValueObjects;
using Domain.Users.Errors;

using MediatR;

namespace Application.Reviews.Comments;

public class CommentReviewCommandHandler(
    IReviewRepository reviewRepository,
    IUserRepository userRepository,
    IAppContext appContext) : IRequestHandler<CommentReviewCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(CommentReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = appContext.GetUserId();

        var review = await reviewRepository.GetReviewByIdAsync(request.ReviewId, cancellationToken);
        if (review is null)
            return Result<Unit>.Failure(ReviewErrors.NotFound(request.ReviewId));

        var matchedComment = review.Comments.FirstOrDefault(x => x.FromUserId == userId);

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return Result<Unit>.Failure(UserErrors.NotFound());

        if (matchedComment is null)
        {
            review.AddComment([Comment.Create(userId, user.Username, request.Comment)]);
        }
        else
        {
            review.RemoveComment(matchedComment);
            review.AddComment([Comment.Create(userId, user.Username, request.Comment)]);
        }

        await reviewRepository.UpdateCommentsAsync(review.Id, [.. review.Comments], cancellationToken);

        return Result<Unit>.Success(new());
    }
}
