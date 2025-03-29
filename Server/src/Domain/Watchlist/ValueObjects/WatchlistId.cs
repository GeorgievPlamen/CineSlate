using Domain.Common.Models;

namespace Domain.Watchlist.ValueObjects;

public class WatchlistId : ValueObject
{
    public Guid Value { get; private set; }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static WatchlistId Create() => new() { Value = Guid.NewGuid() };
    public static WatchlistId Create(Guid value) => new() { Value = value };
}