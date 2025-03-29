
using Application.Common;
using Application.Common.Interfaces;
using Application.Watchlist.Interfaces;

using Domain.Watchlist.Errors;

using MediatR;

namespace Application.Watchlist.GetWatchlist;

public class GetWatchlistQueryHandler(IWatchlistRepository watchlistRepository, IAppContext appContext) : IRequestHandler<GetWatchlistQuery, Result<GetWatchlistResponse>>
{
    public async Task<Result<GetWatchlistResponse>> Handle(GetWatchlistQuery request, CancellationToken cancellationToken)
    {
        var userId = appContext.GetUserId();

        var watchlist = await watchlistRepository.GetWatchlistByUserIdAsync(userId, cancellationToken);

        return watchlist is null
            ? Result<GetWatchlistResponse>.Failure(WatchlistErrors.NotFound())
            : Result<GetWatchlistResponse>.Success(new([.. watchlist.Movies.Select(x => new KeyValuePair<int, bool>(x.MovieId.Value, x.Watched))]));
    }
}
