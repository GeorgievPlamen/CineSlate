using Application.Common;
using MediatR;

namespace Application.Movies.Details;

public class GetMovieDetailsQueryHandler : IRequestHandler<GetMovieDetailsQuery, Result<MovieDetails>>
{
    public Task<Result<MovieDetails>> Handle(GetMovieDetailsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
