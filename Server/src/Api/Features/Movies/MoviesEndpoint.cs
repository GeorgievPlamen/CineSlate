
using Api.Common;
using Application.Common;
using Application.Movies;
using Application.Movies.Popular;
using MediatR;

namespace Api.Features.Movies;

public static class MoviesEndpoint
{
    public static void MapMovies(this WebApplication app)
    {
        var movies = app.MapGroup("api/movies").RequireAuthorization();

        movies.MapGet("/popular", GetPopularMoviesAsync);
    }

    private static async Task<IResult> GetPopularMoviesAsync(int? pageNumber, ISender mediatr, CancellationToken cancellationToken)
    => Response<Paged<Movie>>.Match(await mediatr.Send(new GetPopularMoviesQuery(pageNumber), cancellationToken));
}