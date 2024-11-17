using Application.Common;
using Domain.Movies;
using Domain.Movies.ValueObjects;
using MediatR;

namespace Application.Movies.List;

public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, Result<Paged<MovieAggregate>>>
{
    public async Task<Result<Paged<MovieAggregate>>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        int[] genreIds = [878, 28, 12];

        var movie = MovieAggregate.Create(
            MovieId.Create(912649),
            "Venom: The Last Dance",
            "Eddie and Venom are on the run. Hunted by both of their worlds and with the net closing in, the duo are forced into a devastating decision that will bring the curtains down on Venom and Eddie's last dance.",
            DateTime.Parse("2024-10-22"),
            "/aosm8NMQ3UyoBVpSxyimorCQykC.jpg",
            genreIds.Select(x => Genre.Create(x)));

        return Result<Paged<MovieAggregate>>.Success(new([movie], 1, false, false));
    }
}
