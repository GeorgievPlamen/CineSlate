using Application.Common.Interfaces;
using Domain.Test;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Test;

public class TestCommandHandler : IRequestHandler<TestCommand, TestEntity>
{
    private readonly ILogger<TestCommandHandler> _logger;
    private readonly ITestRepository _testRepository;

    public TestCommandHandler(ILogger<TestCommandHandler> logger, ITestRepository testRepository)
    {
        _logger = logger;
        _testRepository = testRepository;
    }
    public async Task<TestEntity> Handle(TestCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Received request: {request}");

        var testEntities = _testRepository.GetAll();

        _logger.LogInformation(testEntities.ToString());

        return await Task.FromResult(new TestEntity(request.Name));
    }
}