
namespace Domain.Common.Models;

public abstract class Entity<TId>(TId id) : IEquatable<Entity<TId>>, IEntity where TId : notnull
{
    private readonly List<DomainEvent> _domainEvents = [];
    public TId Id { get; private set; } = id;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public string? CreatedBy { get; private set; }
    public string? UpdatedBy { get; private set; }
    public IReadOnlyCollection<DomainEvent> DomainEvents => [.. _domainEvents];

    public void Raise(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearEvents() => _domainEvents.Clear();

    public override bool Equals(object? obj)
        => obj is Entity<TId> entity && Id!.Equals(entity.Id);

    public bool Equals(Entity<TId>? other)
        => Equals((object?)other);

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
        => Equals(left, right);

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
        => !Equals(left, right);

    public override int GetHashCode()
        => Id!.GetHashCode();

    public void SetCreated(string email, DateTimeOffset createdAt)
    {
        CreatedBy = email;
        CreatedAt = createdAt;
    }

    public void SetUpdated(string email, DateTimeOffset updatedAt)
    {
        UpdatedBy = email;
        UpdatedAt = updatedAt;
    }
}