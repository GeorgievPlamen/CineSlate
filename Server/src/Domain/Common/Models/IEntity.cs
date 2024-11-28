namespace Domain.Common.Models;

public interface IEntity
{
    public IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    public void SetCreated(string email, DateTimeOffset createdAt);
    public void SetUpdated(string email, DateTimeOffset updatedAt);
    public void Raise(DomainEvent domainEvent);
    public void ClearEvents();
}