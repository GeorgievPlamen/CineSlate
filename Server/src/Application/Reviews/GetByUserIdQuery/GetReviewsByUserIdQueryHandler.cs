using Application.Common;
using Application.Movies.Interfaces;
using Application.Reviews.Interfaces;
using Application.Users.Interfaces;

using Domain.Common;
using Domain.Users.ValueObjects;

using MediatR;

namespace Application.Reviews.GetByUserIdQuery;

public class GetReviewsByUserIdQueryHandler(
    IReviewRepository reviewRepository,
    IUserRepository userRepository,
    IMovieRepository movieRepository) : IRequestHandler<GetReviewsByUserIdQuery, Result<Paged<ReviewWithMovieDetailsResponse>>>
{
    public async Task<Result<Paged<ReviewWithMovieDetailsResponse>>> Handle(GetReviewsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(request.UserId);

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        
        if (user is null)
            return Result<Paged<ReviewWithMovieDetailsResponse>>.Failure(Error.ServerError());

        var result = await reviewRepository.GetReviewsByAuthorIdAsync(userId, request.Page, Paged.DefaultSize, cancellationToken);
        var movies = await movieRepository.GetManyByIdsAsync(result.Values.Select(x => x.MovieId), cancellationToken);

        var combinedResult = new List<ReviewWithMovieDetailsResponse>();

        foreach (var review in result.Values)
        {
            var matchedMovie = movies.FirstOrDefault(m => m.Id == review.MovieId);

            if (matchedMovie is null)
                return Result<Paged<ReviewWithMovieDetailsResponse>>.Failure(Error.ServerError());

            combinedResult.Add(new(
                matchedMovie.Title,
                matchedMovie.Id.Value,
                matchedMovie.ReleaseDate,
                matchedMovie.PosterPath,
                review.ToResponse(user.Username.Value, review.HasUserLiked(user.Id))));
        }

        return Result<Paged<ReviewWithMovieDetailsResponse>>.Success(new Paged<ReviewWithMovieDetailsResponse>(
            combinedResult,
            result.CurrentPage,
            result.HasNextPage,
            result.HasPreviousPage,
            result.TotalCount
        ));
    }
}
