namespace Domain.Common;

public record Error(string Code, ErrorType Type = ErrorType.ServerError, string? Description = null)
{
    public static Error BadRequest(string message = "Bad Request", string? details = null)
        => new(message, ErrorType.BadRequest, details);

    public static Error Validation(string message, string? details)
        => new(message, ErrorType.Validation, details);
    public static Error NotFound(string message = "Resource not found", string? details = null)
        => new(message, ErrorType.NotFound, details);
    public static Error ServerError(string message = "Server Error", string? details = "Something wrong happened. Server could not handle your request properly.")
        => new(message, ErrorType.ServerError, details);
    public static Error NotAuthorized(string message = "Not Authorized", string? details = "Client not authorized to access resource.")
        => new(message, ErrorType.NotAuthorized, details);
}

public enum ErrorType
{
    BadRequest,
    ServerError,
    Validation,
    NotFound,
    NotAuthorized
}
