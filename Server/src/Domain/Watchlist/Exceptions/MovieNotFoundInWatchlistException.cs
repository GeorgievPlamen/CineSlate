using Domain.Movies.ValueObjects;

namespace Domain.Watchlist.Exceptions;

public class MovieNotFoundInWatchlistException(MovieId movieId)
    : InvalidOperationException($"Movie with id {movieId.Value} not found in watchlist.");
