using Application.Common;
using Application.Movies.Interfaces;
using Domain.Movies.Errors;
using Domain.Movies.ValueObjects;
using MediatR;

namespace Application.Movies.Details;

public class GetMovieDetailsQueryHandler(IMovieRepository movieRepository, IMoviesClient moviesClient) : IRequestHandler<GetMovieDetailsQuery, Result<MovieDetails>>
{
    public async Task<Result<MovieDetails>> Handle(GetMovieDetailsQuery request, CancellationToken cancellationToken)
    {
        var id = MovieId.Create(request.Id);

        var movie = await movieRepository.GetByIdAsync(id, cancellationToken);
        if (movie is null)
            return Result<MovieDetails>.Failure(MovieErrors.NotFound(id));

        if (movie.Details is not null)
            return Result<MovieDetails>.Success(movie.ToMovieDetails());

        return default!;
    }
}
