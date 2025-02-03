using System.Security.Claims;

using Domain.Users.ValueObjects;

using Microsoft.AspNetCore.Http;

namespace Application.Common.Context;

public class ContextHelper(IHttpContextAccessor httpContextAccessor)
{
    public UserId GetUserId()
    {
        var httpContext = httpContextAccessor.HttpContext ?? throw new InvalidOperationException("Http context is null.");

        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User Id was not found in the claims");

        return UserId.Create(Guid.Parse(userId));
    }
}