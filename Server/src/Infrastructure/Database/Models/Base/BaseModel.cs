namespace Infrastructure.Database.Models.Base;

public abstract class BaseModel : IModel
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

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