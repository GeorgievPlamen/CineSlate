using FluentValidation;

namespace Application.Reviews.GetOwnedByMovieId;

public class GetOwnedReviewsByMovieIdQueryValidator : AbstractValidator<GetOwnedReviewsByMovieIdQuery>
{
    public GetOwnedReviewsByMovieIdQueryValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty()
            .Must(value => value > 0).WithMessage("Id must be positive!");
    }
}