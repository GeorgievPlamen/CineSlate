
using Application.Common;
using Application.Common.Interfaces;
using Application.Reviews.Interfaces;
using Application.Users.Interfaces;

using Domain.Movies.Reviews.Errors;
using Domain.Movies.Reviews.ValueObjects;
using Domain.Users.Errors;

using MediatR;

namespace Application.Reviews.Likes;

public class LikeReviewCommandHandler(
    IReviewRepository reviewRepository,
    IUserRepository userRepository,
    IAppContext appContext) : IRequestHandler<LikeReviewCommand, Result<ReviewDetailsResponse>>
{
    public async Task<Result<ReviewDetailsResponse>> Handle(LikeReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = appContext.GetUserId();

        var review = await reviewRepository.GetReviewByIdAsync(request.ReviewId, cancellationToken);
        if (review is null)
            return Result<ReviewDetailsResponse>.Failure(ReviewErrors.NotFound(request.ReviewId));

        var matchedLike = review.Likes.FirstOrDefault(x => x.FromUserId == userId);

        var hasUserLiked = false;

        if (matchedLike is null)
        {
            var user = await userRepository.GetByIdAsync(userId, cancellationToken);
            if (user is null)
                return Result<ReviewDetailsResponse>.Failure(UserErrors.NotFound());

            review.AddLikes([Like.Create(userId, user.Username)]);
            hasUserLiked = true;
        }
        else
        {
            review.RemoveLike(matchedLike);
        }

        await reviewRepository.UpdateLikesAsync(request.ReviewId, [.. review.Likes], cancellationToken);

        return Result<ReviewDetailsResponse>.Success(review.ToDetailsResponse(hasUserLiked, review.Likes.Select(x => x.FromUser).ToList()));
    }
}
