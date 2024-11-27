using Application.Common;
using Application.Movies.Interfaces;
using Domain.Movies.Errors;
using Domain.Movies.ValueObjects;
using MediatR;

namespace Application.Movies.Details;

public class GetMovieDetailsQueryHandler(IMovieRepository movieRepository, IMoviesClient moviesClient) : IRequestHandler<GetMovieDetailsQuery, Result<MovieFull>>
{
    public async Task<Result<MovieFull>> Handle(GetMovieDetailsQuery request, CancellationToken cancellationToken)
    {
        var id = MovieId.Create(request.Id);

        var movie = await movieRepository.GetByIdAsync(id, cancellationToken);

        if (movie is null)
            return Result<MovieFull>.Failure(MovieErrors.NotFound(id));

        if (movie.Details.IsFull())
            return Result<MovieFull>.Success(movie.ToMovieFull());

        var movieDetailed = await moviesClient.GetMovieDetailsAsync(id.Value);

        movie.AddDetails(MovieDetails.Create(
            movieDetailed.BackdropPath,
            movieDetailed.Budget,
            movieDetailed.Homepage,
            movieDetailed.ImdbId,
            movieDetailed.OriginCountry,
            movieDetailed.Revenue,
            movieDetailed.Runtime,
            movieDetailed.Status,
            movieDetailed.Tagline));

        await movieRepository.UpdateAsync(movie, cancellationToken);

        return Result<MovieFull>.Success(movie.ToMovieFull());
    }
}
