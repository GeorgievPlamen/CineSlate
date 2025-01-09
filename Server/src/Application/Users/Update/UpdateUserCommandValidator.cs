using FluentValidation;

namespace Application.Users.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty();

        RuleFor(u => u.Bio)
            .MaximumLength(200);
    }
}