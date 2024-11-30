using Application.Common;
using Application.Movies.Interfaces;
using Domain.Common;
using Domain.Movies.Errors;
using Domain.Movies.ValueObjects;
using MediatR;

namespace Application.Movies.Details;

public class GetMovieDetailsQueryHandler(IMovieRepository movieRepository, IMovieClient moviesClient) : IRequestHandler<GetMovieDetailsQuery, Result<MovieDetailed>>
{
    public async Task<Result<MovieDetailed>> Handle(GetMovieDetailsQuery request, CancellationToken cancellationToken)
    {
        var id = MovieId.Create(request.Id);

        var movie = await movieRepository.GetByIdAsync(id, cancellationToken);

        if (movie is null)
            return Result<MovieDetailed>.Failure(MovieErrors.NotFound(id));

        if (movie.Details.IsFull())
            return Result<MovieDetailed>.Success(movie.ToMovieDetailed());

        var movieDetailed = await moviesClient.GetMovieDetailsAsync(id.Value, cancellationToken);

        if (movieDetailed is null)
            return Result<MovieDetailed>.Failure(Error.ServerError());

        movie.AddDetails(
            MovieDetails.Create(
                movieDetailed.BackdropPath,
                movieDetailed.Budget,
                movieDetailed.Homepage,
                movieDetailed.ImdbId,
                movieDetailed.OriginCountry,
                movieDetailed.Revenue,
                movieDetailed.Runtime,
                movieDetailed.Status,
                movieDetailed.Tagline),
            movieDetailed.Genres
                .Select(g => Genre.Create(g.Id, g.Name)));

        await movieRepository.UpdateAsync(movie, cancellationToken);

        return Result<MovieDetailed>.Success(movie.ToMovieDetailed());
    }
}
