using Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers.Common;

[ApiController]
[Route("/api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult Problem(IReadOnlyList<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return Problem(errors[0]);
    }

    private IActionResult ValidationProblem(IReadOnlyList<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description ?? "Not valid.");
        }

        return ValidationProblem(modelStateDictionary);
    }

    private IActionResult Problem(Error firstError)
    {
        var statusCode = firstError.Type switch
        {
            ErrorType.BadRequest => StatusCodes.Status400BadRequest,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.ServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status418ImATeapot
        };

        return Problem(statusCode: statusCode, title: firstError.Code, detail: firstError.Description);
    }
}
