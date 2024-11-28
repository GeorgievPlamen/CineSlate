namespace Infrastructure.Database.Models;

public class GenreModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<MovieModel> Movies { get; set; } = [];
}