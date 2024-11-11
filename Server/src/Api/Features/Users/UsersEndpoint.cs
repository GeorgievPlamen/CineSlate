using Api.Common;
using Api.Features.Users.Requests;
using Application.Users.Login;
using Application.Users.Me;
using Application.Users.Register;
using MediatR;

namespace Api.Features.Users;

public static class UsersEndpoint
{
    public static void MapUsers(this WebApplication app)
    {
        var users = app.MapGroup("/api/users");

        users.MapGet("/me", GetMeAsync).RequireAuthorization();

        users.MapPost("/login", LoginAsync);
        users.MapPost("/register", RegisterAsync);
    }

    public static async Task<IResult> GetMeAsync(ISender mediatr, CancellationToken cancellationToken)
        => Response<MeResponse>.Match(await mediatr.Send(new MeQuery(), cancellationToken));

    public static async Task<IResult> LoginAsync(LoginRequest request, ISender mediatr, CancellationToken cancellationToken)
        => Response<LoginResponse>.Match(await mediatr.Send(new LoginCommand(
            request.Email,
            request.Password),
            cancellationToken));

    public static async Task<IResult> RegisterAsync(RegisterRequest request, ISender mediatr, CancellationToken cancellationToken)
        => Response<Unit>.Match(await mediatr.Send(new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password),
            cancellationToken));
}