using Api.Common;
using Api.Features.Users.Requests;
using Application.Users.Login;
using Application.Users.Me;
using Application.Users.Register;
using Domain.Users.ValueObjects;
using MediatR;

namespace Api.Features.Users;

public static class UsersEndpoint
{
    public const string Uri = "/api/users";
    public const string Me = "/me";
    public const string Login = "/login";
    public const string Register = "/register";

    public static void MapUsers(this WebApplication app)
    {
        var users = app.MapGroup(Uri);

        users.MapGet(Me, GetMeAsync).RequireAuthorization();

        users.MapPost(Login, LoginAsync);
        users.MapPost(Register, RegisterAsync).WithName(Uri + Register);
    }

    public static async Task<IResult> GetMeAsync(ISender mediatr, CancellationToken cancellationToken)
        => Response<MeResponse>.Match(await mediatr.Send(new MeQuery(), cancellationToken));

    public static async Task<IResult> LoginAsync(LoginRequest request, ISender mediatr, CancellationToken cancellationToken)
        => Response<LoginResponse>.Match(await mediatr.Send(new LoginCommand(
            request.Email,
            request.Password),
            cancellationToken));

    public static async Task<IResult> RegisterAsync(RegisterRequest request, ISender mediatr, CancellationToken cancellationToken)
        => Response<UserId>.Match(await mediatr.Send(new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password),
            cancellationToken), Uri + Register);
}
