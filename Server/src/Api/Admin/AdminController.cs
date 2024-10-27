using Api.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin;

[Authorize(Roles = "Admin")]
public class AdminController : BaseController
{
    [HttpGet]
    public IActionResult Test()
    {
        return Ok();
    }
}