namespace Application.Common;

public record Paged<T>(List<T> Values, int TotalCount, bool HasNextPage, bool HasPreviousPage);