using Infrastructure.Database.Models.Base;

namespace Infrastructure.Database.Models;

public class RefreshTokenModel : BaseModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Value { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}
