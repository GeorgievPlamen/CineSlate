namespace Domain.Common;

public record Error(string Code, string? Description = null)
{
    public static readonly Error None = new(string.Empty);
    public static Error Validation(string message, string? details) => new(message, details);
};

