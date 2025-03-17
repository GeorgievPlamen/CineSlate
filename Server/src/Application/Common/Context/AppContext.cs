using System.Security.Claims;

using Application.Common.Interfaces;

using Domain.Users.ValueObjects;

using Microsoft.AspNetCore.Http;

namespace Application.Common.Context;

public class AppContext(IHttpContextAccessor httpContextAccessor) : IAppContext
{
    public UserId GetUserId()
    {
        var httpContext = httpContextAccessor.HttpContext ?? throw new InvalidOperationException("Http context is null.");

        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        return userId is not null ? UserId.Create(Guid.Parse(userId)) : UserId.Create(Guid.Empty);
    }
}