using Application.Common;
using MediatR;

namespace Application.Movies.Popular;

public record GetPopularMoviesQuery(int? Page) : IRequest<Result<Paged<Movie>>>;