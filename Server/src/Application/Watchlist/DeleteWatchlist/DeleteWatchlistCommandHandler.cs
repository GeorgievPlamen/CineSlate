
using Api.Common.Interfaces;

using Application.Common;
using Application.Common.Interfaces;
using Application.Users.Interfaces;
using Application.Watchlist.Interfaces;

using Domain.Common;
using Domain.Users.Errors;
using Domain.Watchlist.Errors;

using MediatR;

namespace Application.Watchlist.DeleteWatchlist;

public class DeleteWatchlistCommandHandler(
    IWatchlistRepository watchlistRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IAppContext appContext) : IRequestHandler<DeleteWatchlistCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteWatchlistCommand request, CancellationToken cancellationToken)
    {
        var userId = appContext.GetUserId();

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var watchlist = await watchlistRepository.GetWatchlistByUserIdAsync(userId, cancellationToken);
        if (watchlist is null)
            return Result<Unit>.Failure(WatchlistErrors.NotFound());

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return Result<Unit>.Failure(UserErrors.NotFound());

        var success = await watchlistRepository.DeleteWatchlistAsync(watchlist.Id, cancellationToken);

        if (success)
        {
            user.RemoveWatchlist();
            success = await userRepository.UpdateAsync(user, cancellationToken);
        }

        if (success)
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        else
            await unitOfWork.RollbackTransactionAsync(cancellationToken);

        return success
        ? Result<Unit>.Success(new())
        : Result<Unit>.Failure(Error.ServerError());
    }
}
