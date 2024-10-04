using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.PipelineBehaviours;

public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger) :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>

{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received: {Request}", request);

        var result = await next();

        _logger.LogInformation("Handled: {Result}", result);
        return result;
    }
}
