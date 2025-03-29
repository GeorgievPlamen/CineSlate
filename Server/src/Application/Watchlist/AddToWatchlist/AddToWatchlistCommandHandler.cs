using Application.Common;
using Application.Common.Interfaces;
using Application.Movies.Interfaces;
using Application.Users.Interfaces;
using Application.Watchlist.Interfaces;

using Domain.Common;
using Domain.Movies.Errors;
using Domain.Movies.ValueObjects;
using Domain.Users.Errors;
using Domain.Watchlist;
using Domain.Watchlist.Errors;

using MediatR;

namespace Application.Watchlist.AddToWatchlist;

public class AddToWatchlistCommandHandler(
    IUserRepository userRepository,
    IMovieRepository movieRepository,
    IWatchlistRepository watchlistRepository,
    IAppContext appContext) : IRequestHandler<AddToWatchlistCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(AddToWatchlistCommand request, CancellationToken cancellationToken)
    {
        var userId = appContext.GetUserId();

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
            return Result<Unit>.Failure(UserErrors.NotFound());

        var movieId = MovieId.Create(request.MovieId);
        var movie = movieRepository.GetByIdAsync(movieId, cancellationToken);

        if (movie is null)
            return Result<Unit>.Failure(MovieErrors.NotFound(movieId));

        if (user.WatchlistId is null)
        {
            var newWatchlist = WatchlistAggregate.Create(userId);
            user.AddWatchlist(newWatchlist.Id);
            newWatchlist.AddMovie(movieId);

            await userRepository.UpdateAsync(user, cancellationToken);

            return await watchlistRepository.CreateWatchlistAsync(newWatchlist, cancellationToken)
            ? Result<Unit>.Success(new())
            : Result<Unit>.Failure(Error.ServerError());
        }

        var watchlist = await watchlistRepository.GetWatchlistAsync(user.WatchlistId, cancellationToken);

        if (watchlist is null)
            return Result<Unit>.Failure(WatchlistErrors.NotFound(user.WatchlistId));

        watchlist.AddMovie(movieId);

        return await watchlistRepository.UpdateWatchlistAsync(watchlist, cancellationToken)
        ? Result<Unit>.Success(new())
        : Result<Unit>.Failure(Error.ServerError());
    }
}