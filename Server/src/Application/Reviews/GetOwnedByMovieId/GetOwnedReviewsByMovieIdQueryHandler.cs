using System.Security.Claims;

using Application.Common;
using Application.Reviews.Interfaces;
using Application.Users.Interfaces;

using Domain.Common;
using Domain.Movies.ValueObjects;
using Domain.Users.ValueObjects;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Application.Reviews.GetOwnedByMovieId;

public class GetOwnedReviewsByMovieIdQueryHandler(
    IReviewRepository reviewRepository,
    IUserRepository userRepository,
    IHttpContextAccessor httpContextAccessor) :
    IRequestHandler<GetOwnedReviewsByMovieIdQuery, Result<ReviewResponse>>
{

    public async Task<Result<ReviewResponse>> Handle(GetOwnedReviewsByMovieIdQuery request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null)
            return Result<ReviewResponse>.Failure(Error.ServerError());

        var userId = UserId.Create(
            Guid.Parse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!));

        var result = await reviewRepository.GetReviewByAuthorIdAndMovieIdAsync(userId, MovieId.Create(request.MovieId), cancellationToken);

        if (result is null)
            return Result<ReviewResponse>.Failure(Error.NotFound());

        var email = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        if (email is null)
            return Result<ReviewResponse>.Failure(Error.ServerError());

        var user = await userRepository.GetAsync(email.Value, cancellationToken);
        if (user is null)
            return Result<ReviewResponse>.Failure(Error.ServerError());

        return Result<ReviewResponse>.Success(result.ToResponse(user.Username.Value, result.HasUserLiked(user.Id)));
    }
}
