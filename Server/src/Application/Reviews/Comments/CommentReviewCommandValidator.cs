using FluentValidation;

namespace Application.Reviews.Comments;

public class CommentReviewCommandValidator : AbstractValidator<CommentReviewCommand>
{
    public CommentReviewCommandValidator()
    {
        RuleFor(x => x.ReviewId)
            .NotEmpty();

        RuleFor(x => x.Comment)
            .NotEmpty();
    }
}