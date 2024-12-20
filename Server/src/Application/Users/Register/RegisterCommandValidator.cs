using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Users.Register;

public partial class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(x => x.Username)
            .NotEmpty()
            .MaximumLength(100);


        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(PasswordRegex());

    }

    [GeneratedRegex("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\\W)(?!.* ).{8,30}$")]
    private static partial Regex PasswordRegex();
}