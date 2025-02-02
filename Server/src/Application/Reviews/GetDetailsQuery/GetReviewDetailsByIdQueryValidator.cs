using FluentValidation;

namespace Application.Reviews.GetDetailsQuery;

public class GetReviewDetailsByIdQueryValidator : AbstractValidator<GetReviewDetailsByIdQuery>
{
    public GetReviewDetailsByIdQueryValidator()
    {
        RuleFor(x => x.ReviewId)
            .NotEmpty();
    }
}