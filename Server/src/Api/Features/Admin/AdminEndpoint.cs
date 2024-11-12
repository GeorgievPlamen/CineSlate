using Domain.Users.Enums;

namespace Api.Features.Admin;

public static class AdminEndpoint
{
    public static void MapAdmin(this WebApplication app)
    {
        var admin = app.MapGroup("api/admin").RequireAuthorization(policy => policy.RequireRole(Roles.Admin.ToString()));

        admin.MapGet("/", () => TypedResults.Ok("For admins only"));
    }
}