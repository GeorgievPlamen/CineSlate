namespace Infrastructure.Database.Models;

public class MovieModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateOnly ReleaseDate { get; set; }
    public string PosterPath { get; private set; } = null!;
    public string BackdropPath { get; private set; } = null!;
    public long Budget { get; set; }
    public string Homepage { get; private set; } = null!;
    public string ImdbId { get; private set; } = null!;
    public string OriginCountry { get; private set; } = null!;
    public long Revenue { get; set; }
    public int Runtime { get; set; }
    public string Status { get; private set; } = null!;
    public string Tagline { get; private set; } = null!;
    public ICollection<GenreModel> Genres { get; set; } = [];
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}