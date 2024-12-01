using FluentValidation;

namespace Application.Movies.Details;

public class GetMovieDetailsQueryValidator : AbstractValidator<GetMovieDetailsQuery>
{
    public GetMovieDetailsQueryValidator()
    {
        RuleFor(x => x.Id)
            .Must(i => i > 0)
            .WithMessage("Id must be positive!");
    }
}