
using Application.Common;
using Application.Common.Interfaces;
using Application.Movies.Interfaces;
using Application.Watchlist.Interfaces;

using Domain.Watchlist.Errors;

using MediatR;

namespace Application.Movies.FromWatchlist;

public class GetMoviesFromWatchlistQueryHandler(
    IAppContext appContext,
    IMovieRepository movieRepository,
    IWatchlistRepository watchlistRepository) : IRequestHandler<GetMoviesFromWatchlistQuery, Result<List<Movie>>>
{
    public async Task<Result<List<Movie>>> Handle(GetMoviesFromWatchlistQuery request, CancellationToken cancellationToken)
    {
        var userId = appContext.GetUserId();

        var watchlist = await watchlistRepository.GetWatchlistByUserIdAsync(userId, cancellationToken);
        if (watchlist is null)
            return Result<List<Movie>>.Failure(WatchlistErrors.NotFound());

        var movies = await movieRepository.GetManyByIdsAsync(watchlist.Movies.Select(x => x.MovieId), cancellationToken);

        return Result<List<Movie>>.Success([.. movies.Select(m => m.ToMovie())]);
    }
}
