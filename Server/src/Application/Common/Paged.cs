namespace Application.Common;

public record Paged<T>(
    List<T> Values,
    int CurrentPage = 1,
    bool HasNextPage = false,
    bool HasPreviousPage = false,
    int TotalCount = 0);

public static class Paged
{
    public const int DefaultSize = 20;
}