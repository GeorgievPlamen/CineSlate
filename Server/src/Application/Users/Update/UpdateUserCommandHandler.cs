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

        if (!string.IsNullOrWhiteSpace(request.Bio)) user.UpdateBio(request.Bio);

        if (!string.IsNullOrWhiteSpace(request.ImageBase64)) user.UpdateProfilePicture(request.ImageBase64);

        var success = await userRepository.UpdateAsync(user, cancellationToken);

        return success
            ? Result<MeResponse>.Success(new(user.Username.Value, user.Email, user.Id.Value, user.Bio ?? "", user.AvatarBase64 ?? ""))
            : Result<MeResponse>.Failure(Error.ServerError());
    }
}
