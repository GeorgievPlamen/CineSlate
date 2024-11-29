namespace Domain.Common.Models;

public abstract class Entity<TId>(TId id) : IEquatable<Entity<TId>>, IEntity where TId : notnull
{
    private readonly List<DomainEvent> _domainEvents = [];
    public TId Id { get; private set; } = id;
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
}