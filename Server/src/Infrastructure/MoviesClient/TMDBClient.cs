using System.Text.Json;
using Application.Common;
using Application.Movies;
using Application.Movies.Interfaces;
using Domain.Movies.ValueObjects;
using Infrastructure.Common.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.MoviesClient;

public class TMDBClient : IMovieClient
{
    private const string TMDBurl = "https://api.themoviedb.org/";
    private readonly HttpClient _httpClient;
    private readonly ApiKeys _apiKeys;
    private static readonly JsonSerializerOptions _jsonSerializerOptions =
        new() { PropertyNameCaseInsensitive = true };
    private string UriForMoviesWithKey(int pageNumber, MoviesBy? getBy = MoviesBy.now_playing)
    => $"/3/movie/{getBy}?page={pageNumber}&api_key={_apiKeys.TMDBKey}";

    public TMDBClient(IHttpClientFactory httpClientFactory, IOptions<ApiKeys> apiKeyOptions)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(TMDBurl);
        _apiKeys = apiKeyOptions.Value;
    }

    public async Task<Paged<Movie>> GetMoviesByPageAsync(MoviesBy moviesBy, int pageNumber)
    {
        var response = await _httpClient.GetAsync(
            UriForMoviesWithKey(pageNumber, moviesBy));

        var movies = JsonSerializer.Deserialize<TMDBMovies>(
            await response.Content.ReadAsStringAsync(),
            _jsonSerializerOptions);

        if (movies is null || movies.Results is null) return new([]);

        return new(movies.Results.Select(
            x => new Movie(
                x.Id,
                x.Title,
                x.Overview,
                x.Release_date,
                x.Poster_path,
                [.. x.Genre_ids.Select(id => Genre.Create(id))])).ToList(),
            movies.Page,
            movies.Page < 500,
            movies.Page > 1,
            movies.Total_Results);
    }

    public Task<Application.Movies.MovieFull> GetMovieDetailsAsync(int id)
    {
        throw new NotImplementedException();
    }

}