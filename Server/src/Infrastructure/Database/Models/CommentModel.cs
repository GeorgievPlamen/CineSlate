using Domain.Users.ValueObjects;

using Infrastructure.Database.Models.Base;

namespace Infrastructure.Database.Models;

public class CommentModel : BaseModel
{
    public Guid Id { get; set; }
    public ReviewModel Review { get; set; } = null!;
    public Guid UserId { get; set; }
    public Username Username { get; set; } = null!;
    public string Comment { get; set; } = null!;
}