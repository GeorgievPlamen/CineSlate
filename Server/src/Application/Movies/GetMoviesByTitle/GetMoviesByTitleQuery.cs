using Application.Common;

using MediatR;

namespace Application.Movies.GetMoviesByTitle;

public record GetMoviesByTitleQuery(string SearchCriteria, int Page) : IRequest<Result<Paged<Movie>>>;