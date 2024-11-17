using Domain.Common.Models;

namespace Domain.Movies.ValueObjects;

public class MovieId : ValueObject
{
    public int Value { get; private set; }
    private MovieId() { }

    public static MovieId Create(int movieId) => new() { Value = movieId };
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
