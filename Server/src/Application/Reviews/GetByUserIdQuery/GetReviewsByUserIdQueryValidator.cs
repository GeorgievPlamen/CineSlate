using FluentValidation;

namespace Application.Reviews.GetByUserIdQuery;

public class GetReviewsByUserIdQueryValidator : AbstractValidator<GetReviewsByUserIdQuery>
{
    public GetReviewsByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}