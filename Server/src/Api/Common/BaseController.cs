using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Common;

[ApiController]
[Route("/api/[controller]")]
public abstract class BaseController : ControllerBase
{ }
