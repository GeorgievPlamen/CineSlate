using Api.Controllers.Common;
using Application.Users.Login;
using Application.Users.Register;
using Api.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Api.Controllers.Users;

public class UsersController(ILogger<UsersController> logger, ISender mediatr, IMapper mapper) : BaseController
{
    private readonly ILogger<UsersController> _logger = logger;
    private readonly ISender _mediatr = mediatr;
    private readonly IMapper _mapper = mapper;

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        _logger.LogInformation("Received request: " + registerRequest.ToString());

        var result = await _mediatr.Send(_mapper.Map<RegisterCommand>(registerRequest));

        _logger.LogInformation("Result from app: " + result.ToString());

        if (result.IsFailure)
        {
            return BadRequest(new ProblemDetails()
            {
                Title = result.Errors[0].Code,
                Detail = result.Errors[0].Description,
            });
        }

        return Ok(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        _logger.LogInformation("Received request: " + loginRequest.ToString());

        var result = await _mediatr.Send(_mapper.Map<LoginQuery>(loginRequest));

        _logger.LogInformation("Result from app: " + result.ToString());

        return Ok(result);
    }
}