
using Api.Common;

using Application.Watchlist.AddToWatchlist;
using Application.Watchlist.GetWatchlist;
using Application.Watchlist.RemoveFromWatchlist;

using MediatR;

namespace Api.Features.Watchlist;

public static class WatchlistEndpoint
{
    public const string Uri = "/api/watchlist";

    public static void MapWatchlist(this WebApplication app)
    {
        var watchlist = app.MapGroup(Uri).RequireAuthorization();

        watchlist.MapGet("/", GetWatchlistAsync);
        watchlist.MapPost("/{movieId}", AddToWatchlistAsync);
        watchlist.MapPost("/remove", RemoveFromWatchlistAsync);
    }

    private static async Task<IResult> GetWatchlistAsync(ISender mediator, CancellationToken cancellationToken)
        => Response<GetWatchlistResponse>.Match(await mediator.Send(new GetWatchlistQuery(), cancellationToken));

    private static async Task<IResult> AddToWatchlistAsync(int movieId, ISender mediatr, CancellationToken cancellationToken)
        => Response<Unit>.Match(await mediatr.Send(new AddToWatchlistCommand(movieId), cancellationToken));

    private static async Task<IResult> RemoveFromWatchlistAsync(int movieId, bool watched, ISender mediator, CancellationToken cancellationToken)
        => Response<Unit>.Match(await mediator.Send(new RemoveFromWatchlistCommand(movieId, watched), cancellationToken));
}