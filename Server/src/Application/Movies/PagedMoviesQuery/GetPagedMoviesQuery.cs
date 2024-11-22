using Application.Common;
using MediatR;

namespace Application.Movies.PagedMoviesQuery;

public record GetPagedMoviesQuery(MoviesBy MoviesBy, int? Page) : IRequest<Result<Paged<Movie>>>;