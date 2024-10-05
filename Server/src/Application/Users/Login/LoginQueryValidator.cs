using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Users.Login;

public partial class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();
    }
}