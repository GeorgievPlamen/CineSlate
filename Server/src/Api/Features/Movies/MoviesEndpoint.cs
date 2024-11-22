
using Api.Common;
using Application.Common;
using Application.Movies;
using Application.Movies.PagedMoviesQuery;
using MediatR;

namespace Api.Features.Movies;

public static class MoviesEndpoint
{
    public static void MapMovies(this WebApplication app)
    {
        var movies = app.MapGroup("api/movies").RequireAuthorization();

        movies.MapGet("/{moviesBy}", GetMoviesAsync);
    }

    private static async Task<IResult> GetMoviesAsync(MoviesBy moviesBy, int? pageNumber, ISender mediatr, CancellationToken cancellationToken)
    => Response<Paged<Movie>>.Match(await mediatr.Send(new GetPagedMoviesQuery(moviesBy, pageNumber), cancellationToken));
}