
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

        movies.MapGet("/{id}", GetMovieDetailsByIdAsync);
        movies.MapGet("/now_playing", GetNowPlayingAsync);
        movies.MapGet("/popular", GetPopularAsync);
        movies.MapGet("/top_rated", GetTopRatedAsync);
        movies.MapGet("/upcoming", GetUpcomingAsync);
    }

    private static async Task<IResult> GetMovieDetailsByIdAsync(int id, ISender mediatr, CancellationToken cancellationToken)
        => Response<MovieDetailed>.Match(await mediatr.Send(new GetMovieDetailsQuery(id), cancellationToken));

    private static async Task<IResult> GetNowPlayingAsync(int? page, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<Movie>>.Match(await mediatr.Send(new GetPagedMoviesQuery(MoviesBy.now_playing, page), cancellationToken));

    private static async Task<IResult> GetPopularAsync(int? page, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<Movie>>.Match(await mediatr.Send(new GetPagedMoviesQuery(MoviesBy.popular, page), cancellationToken));

    private static async Task<IResult> GetTopRatedAsync(int? page, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<Movie>>.Match(await mediatr.Send(new GetPagedMoviesQuery(MoviesBy.top_rated, page), cancellationToken));

    private static async Task<IResult> GetUpcomingAsync(int? page, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<Movie>>.Match(await mediatr.Send(new GetPagedMoviesQuery(MoviesBy.upcoming, page), cancellationToken));
}