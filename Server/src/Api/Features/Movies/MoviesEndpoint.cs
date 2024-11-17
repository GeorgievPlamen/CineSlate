
using Api.Common;
using Application.Common;
using Application.Movies.List;
using Domain.Movies;
using MediatR;

namespace Api.Features.Movies;

public static class MoviesEndpoint
{
    public static void MapMovies(this WebApplication app)
    {
        var movies = app.MapGroup("api/movies").RequireAuthorization();

        movies.MapGet("/", GetMoviesAsync);
    }

    private static async Task<IResult> GetMoviesAsync(ISender mediatr, CancellationToken cancellationToken)
    => Response<Paged<MovieAggregate>>.Match(await mediatr.Send(new GetMoviesQuery(), cancellationToken));
}