using Domain.Test;
using MediatR;

namespace Application.Test;

public record TestCommand(string Name) : IRequest<TestEntity>;