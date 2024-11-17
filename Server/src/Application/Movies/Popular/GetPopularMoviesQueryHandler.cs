using Application.Common;
using Application.Movies.Interfaces;
using AutoMapper;
using Domain.Movies;
using Domain.Movies.ValueObjects;
using MediatR;

namespace Application.Movies.Popular;

public class GetPopularMoviesQueryHandler(IMoviesClient moviesClient, IMapper mapper) : IRequestHandler<GetPopularMoviesQuery, Result<Paged<Movie>>>
{
    public async Task<Result<Paged<Movie>>> Handle(GetPopularMoviesQuery request, CancellationToken cancellationToken)
    {
        await moviesClient.GetPopularMoviesByPage(request.Page ?? 1);

        int[] genreIds = [878, 28, 12];

        var movie = MovieAggregate.Create(
            MovieId.Create(912649),
            "Venom: The Last Dance",
            "Eddie and Venom are on the run. Hunted by both of their worlds and with the net closing in, the duo are forced into a devastating decision that will bring the curtains down on Venom and Eddie's last dance.",
            DateOnly.Parse("2024-10-22"),
            "/aosm8NMQ3UyoBVpSxyimorCQykC.jpg",
            genreIds.Select(x => Genre.Create(x)));

        return Result<Paged<Movie>>.Success(new([mapper.Map<Movie>(movie)], 1, false, false));
    }
}
