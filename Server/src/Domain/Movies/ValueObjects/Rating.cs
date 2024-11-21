using Domain.Common.Models;
using Domain.Users.ValueObjects;

namespace Domain.Movies.ValueObjects;

public class Rating : ValueObject
{
    private Rating() { }

    public int Value { get; private set; }
    public UserId RatedBy { get; private set; } = null!;
    
    public static Rating Create(int rating, UserId ratedBy)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "Must be between 1 and 5!");

        return new() { Value = rating, RatedBy = ratedBy };
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return RatedBy;
    }
}
