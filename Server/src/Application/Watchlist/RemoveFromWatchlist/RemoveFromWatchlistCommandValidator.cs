using FluentValidation;

namespace Application.Watchlist.RemoveFromWatchlist;

public class RemoveFromWatchlistCommandValidator : AbstractValidator<RemoveFromWatchlistCommand>
{
    public RemoveFromWatchlistCommandValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty();
    }
}