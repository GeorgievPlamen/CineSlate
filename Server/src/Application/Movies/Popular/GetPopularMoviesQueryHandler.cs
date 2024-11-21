using Application.Common;
using Application.Movies.Interfaces;
using Domain.Common;
using Domain.Movies;
using Domain.Movies.ValueObjects;
using MediatR;

namespace Application.Movies.Popular;

public class GetPopularMoviesQueryHandler(IMoviesClient moviesClient, IMovieRepository moviesRepository) : IRequestHandler<GetPopularMoviesQuery, Result<Paged<Movie>>>
{
    public async Task<Result<Paged<Movie>>> Handle(GetPopularMoviesQuery request, CancellationToken cancellationToken)
    {
        var defaultPage = 1;
        var popularMovies = await moviesClient
            .GetPopularMoviesByPageAsync(request.Page ?? defaultPage);

        if (popularMovies.Values.Count == 0)
            return Result<Paged<Movie>>.Failure(Error.ServerError());

        var popularMovieIds = popularMovies.Values
            .Select(m => MovieId.Create(m.Id))
            .ToList();

        var knownMovies = await moviesRepository
            .GetManyByIdsAsync(popularMovieIds, cancellationToken);

        var unknownMovieIds = popularMovieIds
            .Except(knownMovies.Select(m => m.Id))
            .Select(id => id.Value)
            .ToList();

        var unknownMovies = popularMovies.Values
            .Where(m => unknownMovieIds.Contains(m.Id))
            .ToList();

        var movieAggregates = unknownMovies.Select(x => MovieAggregate.Create(
            MovieId.Create(x.Id),
            x.Title,
            x.Description,
            x.ReleaseDate,
            x.PosterPath,
            x.GenreIds.Select(g => Genre.Create(g)))).ToList();

        if (movieAggregates.Count > 0)
        {
            await moviesRepository.CreateManyAsync(movieAggregates, cancellationToken);
            knownMovies.AddRange(movieAggregates);
        }

        var movies = knownMovies.Select(x => new Movie(
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
