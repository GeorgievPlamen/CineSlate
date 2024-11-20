using MediatR;

namespace Domain.Common.Models;

public abstract record DomainEvent : INotification
{
    public Guid Id { get; } = Guid.NewGuid();
}