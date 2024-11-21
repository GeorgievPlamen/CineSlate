using Domain.Common.Models;

namespace Domain.Movies.ValueObjects;

public class MovieId : ValueObject
{
    private MovieId() { }

    public int Value { get; private set; }
    public static MovieId Create(int movieId) => new() { Value = movieId };
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
