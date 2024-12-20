using Application.Common;
using Application.Reviews.Interfaces;
using Application.Users.Interfaces;
using Domain.Common;
using Domain.Movies.ValueObjects;
using MediatR;

namespace Application.Reviews.GetByMovieId;

public class GetReviewsByMovieIdQueryHandler(IReviewRepository reviewRepository, IUserRepository userRepository) : IRequestHandler<GetReviewsByMovieIdQuery, Result<Paged<ReviewResponse>>>
{

    public async Task<Result<Paged<ReviewResponse>>> Handle(GetReviewsByMovieIdQuery request, CancellationToken cancellationToken)
    {
        var result = await reviewRepository.GetReviewsByMovieIdAsync(MovieId.Create(request.MovieId), request.Page, Paged.DefaultSize, cancellationToken);

        if (result is null)
            return Result<Paged<ReviewResponse>>.Failure(Error.NotFound());

        var users = await userRepository.GetManyByIdAsync(result.Values.Select(r => r.Author), cancellationToken);

        var reviewResponses = result.Values
            .Select(r => r.ToResponse(
                users.FirstOrDefault(u => u.Id.Value == r.Author.Value)?
                .Username.Value ?? "Username not found"))
            .ToList();

        return Result<Paged<ReviewResponse>>.Success(new Paged<ReviewResponse>(
            reviewResponses,
            result.CurrentPage,
            result.HasNextPage,
            result.HasPreviousPage,
            result.TotalCount));
    }
}
