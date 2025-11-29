using FluentValidation;

namespace Application.Watchlist.AddToWatchlist;

public class AddToWatchlistCommandValidator : AbstractValidator<AddToWatchlistCommand>
{
    public AddToWatchlistCommandValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty();
    }
}