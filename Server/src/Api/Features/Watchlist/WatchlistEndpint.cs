
using Api.Common;

using Application.Watchlist.AddToWatchlist;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Watchlist;

public static class WatchlistEndpoint
{
    public const string Uri = "/api/watchlist";

    public static void MapWatchlist(this WebApplication app)
    {
        var watchlist = app.MapGroup(Uri).RequireAuthorization();

        watchlist.MapGet("/", GetWatchlistAsync);
        watchlist.MapPost("/", AddToWatchlistAsync);
        watchlist.MapPut("/", UpdateWatchlistAsync);
        watchlist.MapDelete("/", RemoveFromWatchlistAsync);
    }

    private static async Task<IActionResult> GetWatchlistAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static async Task<IResult> AddToWatchlistAsync(int movieId, ISender mediatr, CancellationToken cancellationToken)
        => Response<Unit>.Match(await mediatr.Send(new AddToWatchlistCommand(movieId), cancellationToken));
    private static async Task<IActionResult> UpdateWatchlistAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static async Task<IActionResult> RemoveFromWatchlistAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }
}