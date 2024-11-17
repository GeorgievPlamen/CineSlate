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
            28 => new Genre() { Value = "Action" },
            12 => new Genre() { Value = "Adventure" },
            16 => new Genre() { Value = "Animation" },
            35 => new Genre() { Value = "Comedy" },
            80 => new Genre() { Value = "Crime" },
            99 => new Genre() { Value = "Documentary" },
            18 => new Genre() { Value = "Drama" },
            10751 => new Genre() { Value = "Family" },
            14 => new Genre() { Value = "Fantasy" },
            36 => new Genre() { Value = "History" },
            27 => new Genre() { Value = "Horror" },
            10402 => new Genre() { Value = "Music" },
            9648 => new Genre() { Value = "Mystery" },
            878 => new Genre() { Value = "Science Fiction" },
            _ => new Genre() { Value = "???" }
        };
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
