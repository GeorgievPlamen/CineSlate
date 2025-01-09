using Application.Common;
using Application.Users.Interfaces;
using Application.Users.Me;
using Domain.Common;
using Domain.Users.Errors;
using MediatR;

namespace Application.Users.Update;

public class UpdateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, Result<MeResponse>>
{
    public async Task<Result<MeResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
            return Result<MeResponse>.Failure(UserErrors.NotFound());

        user.Update(request.Bio);

        var success = await userRepository.UpdateAsync(user, cancellationToken);
        if (!success)
            return Result<MeResponse>.Failure(Error.ServerError());

        return Result<MeResponse>.Success(new(user.Username.Value, user.Email, user.Id.Value, user.Bio));
    }
}
