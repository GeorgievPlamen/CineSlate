namespace Domain.Common.Models;

public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public string? CreatedBy { get; private set; }
    public string? UpdatedBy { get; private set; }

    public override bool Equals(object? obj)
    {
        return obj is Entity entity && Id!.Equals(entity.Id);
    }

    public bool Equals(Entity? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(Entity left, Entity right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id!.GetHashCode();
    }

    public void SetCreated(string email, DateTimeOffset createdAt)
    {
        CreatedBy = email;
        CreatedAt = createdAt;
    }
    public void SetUpdated(string email, DateTimeOffset createdAt)
    {
        UpdatedBy = email;
        UpdatedAt = createdAt;
    }
}