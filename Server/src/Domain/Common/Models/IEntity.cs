namespace Domain.Common.Models;

public interface IEntity
{
    public IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    public void Raise(DomainEvent domainEvent);
    public void ClearEvents();
}