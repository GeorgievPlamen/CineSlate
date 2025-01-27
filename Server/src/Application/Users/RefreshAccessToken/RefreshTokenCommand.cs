using Application.Common;
using Application.Users.Login;

using MediatR;

namespace Application.Users.RefreshAccessToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<Result<LoginResponse>>;