namespace Infrastructure.Database.Models;

public class MovieToWatchModel
{
    public Guid Id { get; set; }
    public WatchlistModel Watchlist { get; set; } = null!;
    public int MovieId { get; set; }
    public bool Watched { get; set; }
}
