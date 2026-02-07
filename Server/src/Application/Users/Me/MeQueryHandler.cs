using System.Security.Claims;

using Application.Common;
using Application.Users.Interfaces;

using Domain.Common;
using Domain.Users.Errors;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Application.Users.Me;

public class MeQueryHandler(
    IHttpContextAccessor httpContextAccessor,
    IUserRepository userRepository) :
    IRequestHandler<MeQuery, Result<MeResponse>>
{
    private readonly HttpContext? _httpContext = httpContextAccessor?.HttpContext;

    public async Task<Result<MeResponse>> Handle(MeQuery request, CancellationToken cancellationToken)
    {
        if (_httpContext is null)
            return Result<MeResponse>.Failure(Error.ServerError());

        var email = _httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        if (email is null)
            return Result<MeResponse>.Failure(UserErrors.NotFound());

        var foundUser = await userRepository.GetAsync(email.Value, cancellationToken);
        if (foundUser is null)
            return Result<MeResponse>.Failure(UserErrors.NotFound());

        var result = new MeResponse(
            foundUser.Username.Value,
            foundUser.Email,
            foundUser.Id.Value,
            foundUser.Bio ?? "",
            foundUser.AvatarBase64 ?? "");

        return Result<MeResponse>.Success(result);
    }
}
