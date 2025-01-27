using Application.Common;

using Domain.Common;

using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Common;

public static class Response<T>
{
    public static IResult Match(Result<T> result)
    {
        return result.IsFailure ? Problem(result.Errors) : TypedResults.Ok(result.Value);
    }

    public static IResult Match(Result<T> result, string createdAtRoute)
    {
        return result.IsFailure ? Problem(result.Errors) : TypedResults.CreatedAtRoute(createdAtRoute, result.Value);
    }

    private static IResult Problem(IReadOnlyList<Error> errors)
    {
        if (errors.All(error => error.Type == ErrorType.Validation))
            return ValidationProblem(errors);

        return Problem(errors[0]);
    }

    private static ProblemHttpResult Problem(Error firstError)
    {
        var statusCode = firstError.Type switch
        {
            ErrorType.BadRequest => StatusCodes.Status400BadRequest,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotAuthorized => StatusCodes.Status401Unauthorized,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.ServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status418ImATeapot
        };

        return TypedResults.Problem(statusCode: statusCode, title: firstError.Code, detail: firstError.Description);
    }

    private static ValidationProblem ValidationProblem(IReadOnlyList<Error> errors)
    {
        var modelStateDictionary = new Dictionary<string, string[]>();

        foreach (var error in errors)
        {
            modelStateDictionary.Add(
                error.Code,
                [error.Description!]);
        }

        return TypedResults.ValidationProblem(modelStateDictionary);
    }
}