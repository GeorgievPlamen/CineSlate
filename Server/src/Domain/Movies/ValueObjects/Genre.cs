using Domain.Common.Models;

namespace Domain.Movies.ValueObjects;

public class Genre : ValueObject
{
    private Genre() { }

    public int Id { get; private set; }
    public string Value { get; private set; } = null!;

    public static Genre Create(int id)
    {
        return id switch
        {
            12 => new Genre() { Id = id, Value = "Adventure" },
            14 => new Genre() { Id = id, Value = "Fantasy" },
            16 => new Genre() { Id = id, Value = "Animation" },
            18 => new Genre() { Id = id, Value = "Drama" },
            27 => new Genre() { Id = id, Value = "Horror" },
            28 => new Genre() { Id = id, Value = "Action" },
            35 => new Genre() { Id = id, Value = "Comedy" },
            36 => new Genre() { Id = id, Value = "History" },
            53 => new Genre() { Id = id, Value = "Thriller" },
            80 => new Genre() { Id = id, Value = "Crime" },
            99 => new Genre() { Id = id, Value = "Documentary" },
            878 => new Genre() { Id = id, Value = "Science Fiction" },
            9648 => new Genre() { Id = id, Value = "Mystery" },
            10402 => new Genre() { Id = id, Value = "Music" },
            10749 => new Genre() { Id = id, Value = "Romance" },
            10751 => new Genre() { Id = id, Value = "Family" },
            _ => new Genre() { Id = id, Value = "???" }
        };
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
