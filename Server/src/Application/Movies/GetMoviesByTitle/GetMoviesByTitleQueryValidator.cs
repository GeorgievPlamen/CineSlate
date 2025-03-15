using FluentValidation;

namespace Application.Movies.GetMoviesByTitle;

public class GetMoviesByTitleQueryValidator : AbstractValidator<GetMoviesByTitleQuery>
{
    public GetMoviesByTitleQueryValidator()
    {
        RuleFor(x => x.SearchCriteria)
            .NotEmpty();
    }
}