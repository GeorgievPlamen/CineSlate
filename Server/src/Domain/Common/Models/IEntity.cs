namespace Domain.Common.Models;

public interface IEntity
{
    public void SetCreated(string email, DateTimeOffset createdAt);
    public void SetUpdated(string email, DateTimeOffset updatedAt);
}