using Application.Common;

using MediatR;

namespace Application.Movies.FromWatchlist;

public record GetMoviesFromWatchlistQuery : IRequest<Result<List<Movie>>>;