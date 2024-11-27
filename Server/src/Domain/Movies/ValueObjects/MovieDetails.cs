using Domain.Common.Models;

namespace Domain.Movies.ValueObjects;

public class MovieDetails : ValueObject
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

    public static MovieDetails Create(
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

    public static MovieDetails CreateEmpty() => new()
    {
        BackdropPath = "",
        Budget = 0,
        Homepage = "",
        ImdbId = "",
        OriginCountry = "",
        Revenue = 0,
        Runtime = 0,
        Status = "",
        Tagline = ""
    };

    public bool IsFull() =>
        BackdropPath.Length > 0 &&
        Budget > 0 &&
        Homepage.Length > 0 &&
        ImdbId.Length > 0 &&
        OriginCountry.Length > 0 &&
        Revenue > 0 &&
        Runtime > 0 &&
        Status.Length > 0 &&
        Tagline.Length > 0;

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
