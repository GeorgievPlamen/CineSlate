using FluentValidation;

namespace Application.Watchlist;

public class AddToWatchlistCommandValidator : AbstractValidator<AddToWatchlistCommand>
{
    public AddToWatchlistCommandValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty();
    }
}