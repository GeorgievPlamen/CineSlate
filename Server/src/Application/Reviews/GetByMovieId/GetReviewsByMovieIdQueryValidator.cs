using FluentValidation;

namespace Application.Reviews.GetByMovieId;

public class GetReviewsByMovieIdQueryValidator : AbstractValidator<GetReviewsByMovieIdQuery>
{
    public GetReviewsByMovieIdQueryValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty()
            .Must(value => value > 0).WithMessage("Id must be positive!");
    }
}