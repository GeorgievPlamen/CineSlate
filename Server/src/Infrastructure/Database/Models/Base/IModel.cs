namespace Infrastructure.Database.Models.Base;

public interface IModel
{
    public void SetCreated(string email, DateTimeOffset createdAt);
    public void SetUpdated(string email, DateTimeOffset updatedAt);
}