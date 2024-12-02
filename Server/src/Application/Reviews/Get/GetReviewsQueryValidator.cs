using FluentValidation;

namespace Application.Reviews.Get;

public class GetReviewsQueryValidator : AbstractValidator<GetReviewsQuery>
{
    public GetReviewsQueryValidator()
    {
        RuleFor(x => x.ReviewsBy)
            .IsInEnum();
    }
}