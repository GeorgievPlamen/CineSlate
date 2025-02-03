using FluentValidation;

namespace Application.Reviews.Likes;

public class LikeReviewCommandValidator : AbstractValidator<LikeReviewCommand>
{
    public LikeReviewCommandValidator()
    {
        RuleFor(x => x.ReviewId.Value)
            .NotEmpty();
    }
}