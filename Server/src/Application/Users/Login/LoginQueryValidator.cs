using FluentValidation;

namespace Application.Users.Login;

public partial class LoginQueryValidator : AbstractValidator<LoginCommand>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();
    }
}