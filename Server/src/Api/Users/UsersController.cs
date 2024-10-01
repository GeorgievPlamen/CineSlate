using Api.Controllers.Common;
using Application.Users.Login;
using Application.Users.Register;
using Api.User.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Users;

public class UsersController(ILogger<UsersController> logger, ISender mediatr) : BaseController
{
    private readonly ILogger<UsersController> _logger = logger;
    private readonly ISender _mediatr = mediatr;

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        _logger.LogInformation("Received request: " + registerRequest.ToString());

        var result = await _mediatr.Send(new RegisterCommand(
            registerRequest.FirstName,
            registerRequest.LastName,
            registerRequest.Email,
            registerRequest.Password));

        _logger.LogInformation("Result from app: " + result.ToString());

        return Ok(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        _logger.LogInformation("Received request: " + loginRequest.ToString());

        var result = await _mediatr.Send(new LoginQuery(loginRequest.Email, loginRequest.Password));

        _logger.LogInformation("Result from app: " + result.ToString());

        return Ok(result);
    }
}