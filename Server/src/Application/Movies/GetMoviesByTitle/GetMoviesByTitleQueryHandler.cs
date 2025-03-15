
using Application.Common;
using Application.Movies.Interfaces;

using Domain.Common;
using Domain.Movies;
using Domain.Movies.ValueObjects;

using MediatR;

namespace Application.Movies.GetMoviesByTitle;

public class GetMoviesByTitleQueryHandler(IMovieClient movieClient, IMovieRepository moviesRepository) : IRequestHandler<GetMoviesByTitleQuery, Result<Paged<Movie>>>
{
    public async Task<Result<Paged<Movie>>> Handle(GetMoviesByTitleQuery request, CancellationToken cancellationToken)
    {
        var movies = await movieClient.GetMoviesByTitle(request.SearchCriteria, request.Page, cancellationToken);

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
            x.GenreIds.Select(id => Genre.Create(id))));

        if (movieAggregates.Any())
        {
            await moviesRepository.CreateManyAsync(movieAggregates, cancellationToken);
            knownMovies.AddRange(movieAggregates);
        }

        var response = knownMovies.Select(x => x.ToMovie()).ToList();

        return Result<Paged<Movie>>.Success(new(
            response,
            movies.CurrentPage,
            movies.HasNextPage,
            movies.HasPreviousPage,
            movies.TotalCount));
    }
}
