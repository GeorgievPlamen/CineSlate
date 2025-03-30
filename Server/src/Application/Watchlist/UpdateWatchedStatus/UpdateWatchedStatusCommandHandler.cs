
using Application.Common;
using Application.Common.Interfaces;
using Application.Watchlist.Interfaces;

using Domain.Common;
using Domain.Movies.ValueObjects;
using Domain.Watchlist.Errors;

using MediatR;

namespace Application.Watchlist.UpdateWatchedStatus;

public class UpdateWatchedStatusCommandHandler(IWatchlistRepository watchlistRepository, IAppContext appContext) : IRequestHandler<UpdateWatchedStatusCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateWatchedStatusCommand request, CancellationToken cancellationToken)
    {
        var userId = appContext.GetUserId();

        var watchlist = await watchlistRepository.GetWatchlistByUserIdAsync(userId, cancellationToken);
        if (watchlist is null)
            return Result<Unit>.Failure(WatchlistErrors.NotFound());

        var movieToWatch = watchlist.Movies.FirstOrDefault(x => x.MovieId.Value == request.MovieId);
        if (movieToWatch is null)
            return Result<Unit>.Failure(WatchlistErrors.MovieNotFound(MovieId.Create(request.MovieId)));

        watchlist.SetWatched(movieToWatch.MovieId);

        return await watchlistRepository.UpdateWatchlistAsync(watchlist, cancellationToken)
        ? Result<Unit>.Success(new())
        : Result<Unit>.Failure(Error.ServerError());
    }
}
