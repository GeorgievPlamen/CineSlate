using Application.Common;
using Application.Reviews.Interfaces;
using Application.Users.Interfaces;
using Domain.Common;
using Domain.Users.ValueObjects;
using MediatR;

namespace Application.Reviews.GetByUserIdQuery;

public class GetReviewsByUserIdQueryHandler(IReviewRepository reviewRepository, IUserRepository userRepository) : IRequestHandler<GetReviewsByUserIdQuery, Result<Paged<ReviewResponse>>>
{
    public async Task<Result<Paged<ReviewResponse>>> Handle(GetReviewsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(request.UserId);

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return Result<Paged<ReviewResponse>>.Failure(Error.ServerError());

        var result = await reviewRepository.GetReviewsByAuthorIdAsync(userId, request.Page, Paged.DefaultSize, cancellationToken);

        return Result<Paged<ReviewResponse>>.Success(new Paged<ReviewResponse>(
            [.. result.Values.Select(r => r.ToResponse(user.Username.Value))],
            result.CurrentPage,
            result.HasNextPage,
            result.HasPreviousPage,
            result.TotalCount
        ));
    }
}
