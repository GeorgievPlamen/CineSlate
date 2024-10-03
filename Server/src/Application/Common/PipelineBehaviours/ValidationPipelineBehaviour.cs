using Domain.Common;
using FluentValidation;
using MediatR;

namespace Application.Common.PipelineBehaviours;

public class ValidationPipelineBehaviour<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationPipelineBehaviour(IValidator<TRequest>? validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        System.Console.WriteLine("Inside Pipeline");

        if (_validator is null)
        {

            return await next();
        }


        var validationResult = await _validator.ValidateAsync(request);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors
            .Select(validationFailure =>
            Error.Validation(
                validationFailure.ErrorMessage,
                $"{validationFailure.PropertyName}: {validationFailure.AttemptedValue}"))
            .ToList();

        var failureResult = CreateFailureResult<TResponse>(errors);

        return failureResult;
    }

    private static T CreateFailureResult<T>(List<Error> errors) where T : class
    {
        var resultType = typeof(T);
        if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
        {
            var valueType = resultType.GetGenericArguments()[0]; // Get T in Result<T>
            var failureMethod = typeof(Result<>).MakeGenericType(valueType)
                .GetMethod(nameof(Result<object>.Failure), [typeof(List<Error>)]);

            if (failureMethod != null)
            {
                return (T)failureMethod.Invoke(null, [errors])!;
            }
        }
        throw new InvalidOperationException("Couldn't resolve type converison.");
    }

}
