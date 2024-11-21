using Application.Common;
using Application.Movies.Interfaces;
using Domain.Common;
using Domain.Movies;
using Domain.Movies.ValueObjects;
using MediatR;

namespace Application.Movies.Popular;

public class GetPopularMoviesQueryHandler(IMoviesClient moviesClient) : IRequestHandler<GetPopularMoviesQuery, Result<Paged<Movie>>>
{
    public async Task<Result<Paged<Movie>>> Handle(GetPopularMoviesQuery request, CancellationToken cancellationToken)
    {
        var popularMovies = await moviesClient.GetPopularMoviesByPageAsync(request.Page ?? 1);

        if (popularMovies.Values.Count == 0)
            return Result<Paged<Movie>>.Failure(Error.ServerError());

        // TODO Query DB for movies data
        // TODO Raise create movie event for id's not matched
        // TODO Combine TMDB data with Our data and return


        var movieAggregates = popularMovies.Values.Select(x => MovieAggregate.Create(
            MovieId.Create(x.Id),
            x.Title,
            x.Description,
            x.ReleaseDate,
            x.PosterPath,
            x.GenreIds.Select(g => Genre.Create(g)))).ToList();

        var movies = movieAggregates.Select(x => new Movie(
            x.Id.Value,
            x.Title,
            x.Description,
            x.ReleaseDate,
            x.PosterPath,
            x.Genres)).ToList();

        return Result<Paged<Movie>>.Success(new(
            movies,
            popularMovies.CurrentPage,
            popularMovies.HasNextPage,
            popularMovies.HasPreviousPage,
            popularMovies.TotalCount));
    }
}
