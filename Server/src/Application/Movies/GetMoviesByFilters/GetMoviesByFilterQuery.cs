using Application.Common;

using MediatR;

namespace Application.Movies.GetMoviesByFilters;

public record GetMoviesByFilterQuery(int PageNumber, int[]? GenreIds = null, int? Year = null) : IRequest<Result<Paged<Movie>>>;