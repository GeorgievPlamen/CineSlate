using FluentValidation;

namespace Application.Movies.PagedMoviesQuery;

public class GetPagedMoviesQueryValidator : AbstractValidator<GetPagedMoviesQuery>
{
    public GetPagedMoviesQueryValidator()
    {
        RuleFor(x => x.MoviesBy)
            .IsInEnum();

        RuleFor(x => x.Page)
            .Must(p => p > 0)
            .WithMessage("Page must be positive!");
    }
}