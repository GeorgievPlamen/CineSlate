using Api.Controllers.Common;
using Application.Users.Login;
using Application.Users.Register;
using Api.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Application.Users.Me;

namespace Api.Users;

public class UsersController(ISender mediatr, IMapper mapper) : BaseController
{
    private readonly ISender _mediatr = mediatr;
    private readonly IMapper _mapper = mapper;

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(_mapper.Map<RegisterCommand>(registerRequest), cancellationToken);

        return result.IsFailure ? Problem(result.Errors) : Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(_mapper.Map<LoginQuery>(loginRequest), cancellationToken);

        return result.IsFailure ? Problem(result.Errors) : Ok(result.Value);
    }


    [HttpGet("Me")]
    public async Task<IActionResult> Me(CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new MeQuery(), cancellationToken);

        return result.IsFailure ? Problem(result.Errors) : Ok(result.Value);
    }
}