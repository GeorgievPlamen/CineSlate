
using Api.Common;
using Application.Users.Me;
using MediatR;

namespace Api.Users;

public static class UsersEndpoint
{
    public static void MapUsers(this WebApplication app)
    {
        var users = app.MapGroup("/api/users2");

        users.MapGet("/Me", GetMeAsync).RequireAuthorization();
    }

    public static async Task<IResult> GetMeAsync(ISender mediatr, CancellationToken cancellationToken)
        => Response<MeResponse>.Match(await mediatr.Send(new MeQuery(), cancellationToken));
    
}