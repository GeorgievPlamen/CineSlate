using Api.Controllers.Common;
using Application.Test;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TestController(ILogger<TestController> logger, ISender sender) : BaseController
{
    private readonly ILogger<TestController> _logger = logger;
    private readonly ISender _sender = sender;

    public async Task<IActionResult> Test([FromQuery] string name)
    {
        var result = await _sender.Send(new TestCommand(name));
        _logger.LogInformation($"Result is: {result}");
        return Ok(result);
    }
}