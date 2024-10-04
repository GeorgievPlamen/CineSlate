using Api.Controllers.Common;
using Application.Users.Login;
using Application.Users.Register;
using Api.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Api.Users;

public class UsersController(ISender mediatr, IMapper mapper) : BaseController
{
    private readonly ISender _mediatr = mediatr;
    private readonly IMapper _mapper = mapper;

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var result = await _mediatr.Send(_mapper.Map<RegisterCommand>(registerRequest));

        return result.IsFailure ? Problem(result.Errors) : Ok(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var result = await _mediatr.Send(_mapper.Map<LoginQuery>(loginRequest));

        return Ok(result);
    }
}