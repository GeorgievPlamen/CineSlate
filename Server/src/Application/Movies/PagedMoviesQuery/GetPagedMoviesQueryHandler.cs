using Application.Common;
using Application.Movies.Interfaces;
using Domain.Common;
using Domain.Movies;
using Domain.Movies.ValueObjects;
using MediatR;

namespace Application.Movies.PagedMoviesQuery;

public class GetPagedMoviesQueryHandler(IMoviesClient moviesClient, IMovieRepository moviesRepository) : IRequestHandler<GetPagedMoviesQuery, Result<Paged<Movie>>>
{
    public async Task<Result<Paged<Movie>>> Handle(GetPagedMoviesQuery request, CancellationToken cancellationToken)
    {
        var defaultPage = 1;
        var movies = await moviesClient
            .GetMoviesByPageAsync(request.MoviesBy, request.Page ?? defaultPage);

        if (movies.Values.Count == 0)
            return Result<Paged<Movie>>.Failure(Error.ServerError());

        var movieIds = movies.Values
            .Select(m => MovieId.Create(m.Id))
            .ToList();

        var knownMovies = await moviesRepository
            .GetManyByIdsAsync(movieIds, cancellationToken);

        var unknownMovieIds = movieIds
            .Except(knownMovies.Select(m => m.Id))
            .Select(id => id.Value)
            .ToList();

        var unknownMovies = movies.Values
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

        var response = knownMovies.Select(x => new Movie(
            x.Id.Value,
            x.Title,
            x.Description,
            x.ReleaseDate,
            x.PosterPath,
            x.Genres)).ToList();

        return Result<Paged<Movie>>.Success(new(
            response,
            movies.CurrentPage,
            movies.HasNextPage,
            movies.HasPreviousPage,
            movies.TotalCount));
    }
}
