using Application.Common;
using MediatR;

namespace Application.Movies.Details;

public record GetMovieDetailsQuery(int Id) : IRequest<Result<MovieDetailed>>;