using Api.Common;
using Api.Features.Users.Requests;

using Application.Common;
using Application.Users.GetLatest;
using Application.Users.GetUsers;
using Application.Users.Login;
using Application.Users.Me;
using Application.Users.Register;
using Application.Users.Update;

using Domain.Users.ValueObjects;

using MediatR;

using Microsoft.AspNetCore.Mvc;

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
        users.MapGet("/{page}", GetLatestUsersAsync);

        users.MapPost("/", GetUsersAsync);
        users.MapPost(Login, LoginAsync);
        users.MapPost(Register, RegisterAsync).WithName(Uri + Register);
        users.MapPut("/{id}", UpdateAsync).RequireAuthorization();
    }

    private static async Task<IResult> UpdateAsync(UpdateUserRequest request, ISender mediatr, CancellationToken cancellationToken)
        => Response<MeResponse>.Match(await mediatr.Send(new UpdateUserCommand(
            UserId.Create(request.Id),
            request.Bio,
            request.PictureBase64),
            cancellationToken));

    private static async Task<IResult> GetLatestUsersAsync(int page, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<UserResponse>>.Match(await mediatr.Send(new GetLatestUsersQuery(page), cancellationToken));

    private static async Task<IResult> GetUsersAsync([FromBody] GetUsersRequest request, ISender mediatr, CancellationToken cancellationToken)
        => Response<List<UserResponse>>.Match(await mediatr.Send(new GetUsersQuery(request.UserIds), cancellationToken));

    public static async Task<IResult> GetMeAsync(ISender mediatr, CancellationToken cancellationToken)
        => Response<MeResponse>.Match(await mediatr.Send(new MeQuery(), cancellationToken));

    public static async Task<IResult> LoginAsync(LoginRequest request, ISender mediatr, CancellationToken cancellationToken)
        => Response<LoginResponse>.Match(await mediatr.Send(new LoginCommand(
            request.Email,
            request.Password),
            cancellationToken));

    public static async Task<IResult> RegisterAsync(RegisterRequest request, ISender mediatr, CancellationToken cancellationToken)
        => Response<UserId>.Match(await mediatr.Send(new RegisterCommand(
            request.Username,
            request.Email,
            request.Password),
            cancellationToken), Uri + Register);
}
