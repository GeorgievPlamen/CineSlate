using Application.Common;
using Domain.Movies;
using MediatR;

namespace Application.Movies.List;

public record GetMoviesQuery : IRequest<Result<Paged<MovieAggregate>>>;