namespace Api.Features.Admin;

public static class AdminEndpoint
{
    public static void MapAdmin(this WebApplication app)
    {
        // TODO: Add admin policy
        var admin = app.MapGroup("api/admin").RequireAuthorization();

        admin.MapGet("/", () => TypedResults.Ok("For admins only"));
    }
}