using Infrastructure.Database.Models.Base;

namespace Infrastructure.Database.Models;

public class ReviewModel : BaseModel
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public MovieModel Movie { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public string Text { get; set; } = null!;
    public bool ContainsSpoilers { get; set; }
}