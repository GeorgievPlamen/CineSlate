using FluentValidation;

namespace Application.Reviews.Delete;

public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
{
    public DeleteReviewCommandValidator()
    {
        RuleFor(x => x.ReviewId)
            .NotNull();
    }
}
