


using Api.Common;

using Application.Common;
using Application.Movies;
using Application.Movies.Details;
using Application.Movies.GetMoviesByFilters;
using Application.Movies.GetMoviesByTitle;
using Application.Movies.PagedMoviesQuery;

using MediatR;

namespace Api.Features.Movies;

public static class MoviesEndpoint
{
    public const string Uri = "api/movies";

    public static void MapMovies(this WebApplication app)
    {
        var movies = app.MapGroup(Uri);

        movies.MapGet("/{id}", GetMovieDetailsByIdAsync);
        movies.MapGet("/now_playing", GetNowPlayingAsync);
        movies.MapGet("/popular", GetPopularAsync);
        movies.MapGet("/top_rated", GetTopRatedAsync);
        movies.MapGet("/upcoming", GetUpcomingAsync);
        movies.MapGet("/search", GetMoviesByTitleAsync);
        movies.MapGet("/filter", GetMoviesByFiltersAsync);
    }

    private static async Task<IResult> GetMoviesByFiltersAsync(int[]? genreIds, int? year, ISender mediatr, CancellationToken cancellationToken, int page = 1)
        => Response<Paged<Movie>>.Match(await mediatr.Send(new GetMoviesByFilterQuery(page, genreIds, year), cancellationToken));

    private static async Task<IResult> GetMoviesByTitleAsync(string title, ISender mediatr, CancellationToken cancellationToken, int page = 1)
        => Response<Paged<Movie>>.Match(await mediatr.Send(new GetMoviesByTitleQuery(title, page), cancellationToken));

    private static async Task<IResult> GetMovieDetailsByIdAsync(int id, ISender mediatr, CancellationToken cancellationToken)
        => Response<MovieDetailed>.Match(await mediatr.Send(new GetMovieDetailsQuery(id), cancellationToken));

    private static async Task<IResult> GetNowPlayingAsync(int? page, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<Movie>>.Match(await mediatr.Send(new GetPagedMoviesQuery(MoviesBy.now_playing, page ?? 1), cancellationToken));

    private static async Task<IResult> GetPopularAsync(int? page, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<Movie>>.Match(await mediatr.Send(new GetPagedMoviesQuery(MoviesBy.popular, page ?? 1), cancellationToken));

    private static async Task<IResult> GetTopRatedAsync(int? page, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<Movie>>.Match(await mediatr.Send(new GetPagedMoviesQuery(MoviesBy.top_rated, page ?? 1), cancellationToken));

    private static async Task<IResult> GetUpcomingAsync(int? page, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<Movie>>.Match(await mediatr.Send(new GetPagedMoviesQuery(MoviesBy.upcoming, page ?? 1), cancellationToken));
}