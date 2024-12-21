using FluentValidation;

namespace Application.Reviews.GetByEmailAndMovieId;

public class GetReviewByEmailAndMovieIdQueryValidator : AbstractValidator<GetReviewByEmailAndMovieIdQuery>
{
    public GetReviewByEmailAndMovieIdQueryValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty()
            .Must(value => value > 0).WithMessage("Id must be positive!");
    }
}