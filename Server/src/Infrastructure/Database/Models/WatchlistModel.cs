using Infrastructure.Database.Models.Base;

namespace Infrastructure.Database.Models;

public class WatchlistModel : BaseModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<MovieToWatchModel> MovieToWatchModels { get; set; } = [];
}