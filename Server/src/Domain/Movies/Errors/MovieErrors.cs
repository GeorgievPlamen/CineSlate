using Domain.Common;
using Domain.Movies.ValueObjects;

namespace Domain.Movies.Errors;

public static class MovieErrors
{
    public static Error NotFound(MovieId id) => Error.NotFound("Movie.NotFound", $"Movie with id: '{id.Value}' is not found.");
}