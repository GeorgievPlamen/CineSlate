using FluentValidation;

namespace Application.Watchlist.UpdateWatchedStatus;

public class UpdateWatchedStatusCommandValidator : AbstractValidator<UpdateWatchedStatusCommand>
{
    public UpdateWatchedStatusCommandValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty();
    }
}