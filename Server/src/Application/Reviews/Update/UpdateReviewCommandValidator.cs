using FluentValidation;

namespace Application.Reviews.Update;

public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);

        RuleFor(x => x.ReviewId)
            .NotEmpty();

        RuleFor(x => x.Text)
            .MaximumLength(2000);

        RuleFor(x => x.ContainsSpoilers)
            .NotNull();
    }
}