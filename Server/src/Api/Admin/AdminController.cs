using Api.Controllers.Common;
using Domain.Users.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin;

[Authorize(Roles = UserRoles.AdminRole)]
public class AdminController : BaseController
{
    [HttpGet]
    public IActionResult Test()
    {
        return Ok();
    }
}