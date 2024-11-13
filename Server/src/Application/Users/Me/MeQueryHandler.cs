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
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<MeResponse>> Handle(MeQuery request, CancellationToken cancellationToken)
    {
        if (_httpContext is null) 
            return Result<MeResponse>.Failure(Error.ServerError());

        var email = _httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

        if (email is null)
            return Result<MeResponse>.Failure(UserErrors.UserNotFound);

        var foundUser = await _userRepository.GetUserAsync(email.Value,cancellationToken);

        if (foundUser is null)
            return Result<MeResponse>.Failure(UserErrors.UserNotFound);

        var result = new MeResponse(
            foundUser.Name.First,
            foundUser.Name.Last,
            foundUser.Email);

        return Result<MeResponse>.Success(result);
    }
}
