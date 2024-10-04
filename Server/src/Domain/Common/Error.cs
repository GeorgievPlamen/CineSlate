namespace Domain.Common;

public record Error(string Code, ErrorType Type = ErrorType.ServerError, string? Description = null)
{
    public static Error BadRequest(string message, string? details)
        => new(message, ErrorType.BadRequest, details);

    public static Error Validation(string message, string? details)
        => new(message, ErrorType.Validation, details);
    public static Error NotFound(string message = "Resource not found", string? details = null)
        => new(message, ErrorType.NotFound, details);
}

public enum ErrorType
{
    BadRequest,
    ServerError,
    Validation,
    NotFound
}
