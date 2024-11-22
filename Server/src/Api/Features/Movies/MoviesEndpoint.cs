
using Api.Common;
using Application.Common;
using Application.Movies;
using Application.Movies.Details;
using Application.Movies.PagedMoviesQuery;
using MediatR;

namespace Api.Features.Movies;

public static class MoviesEndpoint
{
    public static void MapMovies(this WebApplication app)
    {
        var movies = app.MapGroup("api/movies").RequireAuthorization();

        movies.MapGet("/{moviesBy}", GetMoviesAsync);
        movies.MapGet("/{id}", GetMovieDetailsByIdAsync);
    }

    private static async Task<IResult> GetMovieDetailsByIdAsync(int id, ISender mediatr, CancellationToken cancellationToken)
    => Response<MovieDetails>.Match(await mediatr.Send(new GetMovieDetailsQuery(id), cancellationToken));

    private static async Task<IResult> GetMoviesAsync(MoviesBy moviesBy, int? pageNumber, ISender mediatr, CancellationToken cancellationToken)
    => Response<Paged<Movie>>.Match(await mediatr.Send(new GetPagedMoviesQuery(moviesBy, pageNumber), cancellationToken));
}