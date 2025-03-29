
using Application.Common;
using Application.Common.Interfaces;
using Application.Watchlist.Interfaces;

using Domain.Common;
using Domain.Movies.ValueObjects;
using Domain.Watchlist.Errors;

using MediatR;

namespace Application.Watchlist.RemoveFromWatchlist;

public class RemoveFromWatchlistCommandHandler(IWatchlistRepository watchlistRepository, IAppContext appContext) : IRequestHandler<RemoveFromWatchlistCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RemoveFromWatchlistCommand request, CancellationToken cancellationToken)
    {
        var userId = appContext.GetUserId();

        var watchlist = await watchlistRepository.GetWatchlistByUserIdAsync(userId, cancellationToken);
        if (watchlist is null)
            return Result<Unit>.Failure(WatchlistErrors.NotFound());

        watchlist.RemoveMovie(MovieId.Create(request.MovieId), request.Watched);

        var isSuccess = await watchlistRepository.UpdateWatchlistAsync(watchlist, cancellationToken);

        return isSuccess
        ? Result<Unit>.Success(new())
        : Result<Unit>.Failure(Error.ServerError());
    }
}
