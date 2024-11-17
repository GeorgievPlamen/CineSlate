using Domain.Common.Models;

namespace Domain.Movies.ValueObjects;

public class Genre : ValueObject
{
    public string Value { get; private set; } = null!;
    private Genre() { }

    public static Genre Create(int id)
    {
        return id switch
        {
            12 => new Genre() { Value = "Adventure" },
            14 => new Genre() { Value = "Fantasy" },
            16 => new Genre() { Value = "Animation" },
            18 => new Genre() { Value = "Drama" },
            27 => new Genre() { Value = "Horror" },
            28 => new Genre() { Value = "Action" },
            35 => new Genre() { Value = "Comedy" },
            36 => new Genre() { Value = "History" },
            53 => new Genre() { Value = "Thriller" },
            80 => new Genre() { Value = "Crime" },
            99 => new Genre() { Value = "Documentary" },
            878 => new Genre() { Value = "Science Fiction" },
            9648 => new Genre() { Value = "Mystery" },
            10402 => new Genre() { Value = "Music" },
            10749 => new Genre() { Value = "Romance" },
            10751 => new Genre() { Value = "Family" },
            _ => new Genre() { Value = "???" }
        };
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
