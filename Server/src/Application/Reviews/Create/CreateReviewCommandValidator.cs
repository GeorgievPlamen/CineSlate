using FluentValidation;

namespace Application.Reviews.Create;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);

        RuleFor(x => x.MovieId)
            .Must(mi => mi > 0)
            .WithMessage("Id must be positive!");

        RuleFor(x => x.Text)
            .NotEmpty();

        RuleFor(x => x.ContainsSpoilers)
            .NotEmpty();
    }
}