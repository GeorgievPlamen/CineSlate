using System.Security.Claims;
using Application.Common;
using Application.Users.Interfaces;
using Domain.Common;
using Domain.Users.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Users.Me;

public class MeRequestHandler(
    IHttpContextAccessor httpContextAccessor,
    IUserRepository userRepository) :
    IRequestHandler<MeQuery, Result<MeResponse>>
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<MeResponse>> Handle(MeQuery request, CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext is null) 
        {
            return Result<MeResponse>.Failure(Error.ServerError());
        }

        var email = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email);
        var foundUser = await _userRepository.GetUserAsync(email.Value,cancellationToken);

        if (foundUser is null)
        {
            return Result<MeResponse>.Failure(UserErrors.UserNotFound);
        }

        MeResponse result = new(
            foundUser.Name.First,
            foundUser.Name.Last,
            foundUser.Email);

        return Result<MeResponse>.Success(result);
    }
}
