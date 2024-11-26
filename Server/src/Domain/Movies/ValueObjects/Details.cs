using Domain.Common.Models;

namespace Domain.Movies.ValueObjects;

public class Details : ValueObject
{
    public string BackdropPath { get; private set; } = null!;
    public long Budget { get; private set; }
    public string Homepage { get; private set; } = null!;
    public string ImdbId { get; private set; } = null!;
    public string OriginCountry { get; private set; } = null!;
    public long Revenue { get; private set; }
    public int Runtime { get; private set; }
    public string Status { get; private set; } = null!;
    public string Tagline { get; private set; } = null!;

    public static Details Create(
        string backdropPath,
        long budget,
        string homepage,
        string imdbId,
        string originCountry,
        long revenue,
        int runtime,
        string status,
        string tagline) => new()
        {
            BackdropPath = backdropPath,
            Budget = budget,
            Homepage = homepage,
            ImdbId = imdbId,
            OriginCountry = originCountry,
            Revenue = revenue,
            Runtime = runtime,
            Status = status,
            Tagline = tagline
        };

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return BackdropPath;
        yield return Budget;
        yield return Homepage;
        yield return ImdbId;
        yield return OriginCountry;
        yield return Revenue;
        yield return Runtime;
        yield return Status;
        yield return Tagline;
    }
}
