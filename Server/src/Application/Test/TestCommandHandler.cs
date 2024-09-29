using Domain.Test;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Test;

public class TestCommandHandler : IRequestHandler<TestCommand, TestEntity>
{
    private readonly ILogger<TestCommandHandler> _logger;

    public TestCommandHandler(ILogger<TestCommandHandler> logger)
    {
        _logger = logger;
    }
    public async Task<TestEntity> Handle(TestCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Received request: {request}");
        return await Task.FromResult(new TestEntity(request.Name));
    }
}