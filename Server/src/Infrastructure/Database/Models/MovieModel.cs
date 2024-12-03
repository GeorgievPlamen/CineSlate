using Infrastructure.Database.Models.Base;

namespace Infrastructure.Database.Models;

public class MovieModel : BaseModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateOnly ReleaseDate { get; set; }
    public string PosterPath { get; set; } = null!;
    public string BackdropPath { get; set; } = null!;
    public long Budget { get; set; }
    public string Homepage { get; set; } = null!;
    public string ImdbId { get; set; } = null!;
    public string OriginCountry { get; set; } = null!;
    public long Revenue { get; set; }
    public int Runtime { get; set; }
    public string Status { get; set; } = null!;
    public string Tagline { get; set; } = null!;
    public ICollection<GenreModel> Genres { get; set; } = [];
    public ICollection<ReviewModel> Reviews { get; set; } = [];
    public double Rating { get; set; }
}