using Application.Users.Interfaces;
using MediatR;
using Domain.Users;
using Application.Common;
using Domain.Users.Errors;
using Domain.Common;
using Domain.Users.ValueObjects;

namespace Application.Users.Register;

public class RegisterCommandHandler(
    IUserIdentity userIdentity,
    IUserRepository userRepository) : IRequestHandler<RegisterCommand, Result<UserId>>
{
    private readonly IUserIdentity _userIdentity = userIdentity;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<UserId>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetAsync(request.Email, cancellationToken);

        if (existingUser is not null)
        {
            return Result<UserId>.Failure(UserErrors.AlreadyRegistered);
        }

        string passwordHash = _userIdentity.HashPassword(request.Password);

        User user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash);

        bool isSuccess = await _userRepository.CreateAsync(user, cancellationToken);

        return isSuccess ?
            Result<UserId>.Success(user.Id) :
            Result<UserId>.Failure(Error.ServerError());
    }
}