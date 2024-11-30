using Infrastructure.Database.Models.Base;

namespace Infrastructure.Database.Models;

public class GenreModel : BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<MovieModel> Movies { get; set; } = [];
}