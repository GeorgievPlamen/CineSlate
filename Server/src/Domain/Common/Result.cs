namespace Domain.Common;

public class Result<T>
{
    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    private Result(List<Error> errors)
    {
        IsSuccess = false;
        Errors = errors;
    }
    public T? Value { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IReadOnlyList<Error> Errors { get; } = [];

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new([error]);
    public static Result<T> Failure(List<Error> errors) => new(errors);

    public override string ToString()
        => IsSuccess ? Value!.ToString()! : Errors[0].ToString();
}