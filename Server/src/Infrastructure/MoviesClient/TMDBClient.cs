using System.Text.Json;
using Application.Common;
using Application.Movies;
using Application.Movies.Interfaces;
using Infrastructure.Common.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.MoviesClient;

public class TMDBClient : IMoviesClient
{
    private const string TMDBurl = "https://api.themoviedb.org/";
    private readonly HttpClient _httpClient;
    private readonly ApiKeys _apiKeys;
    private static readonly JsonSerializerOptions _jsonSerializerOptions =
        new() { PropertyNameCaseInsensitive = true };

    public TMDBClient(IHttpClientFactory httpClientFactory, IOptions<ApiKeys> apiKeyOptions)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(TMDBurl);
        _apiKeys = apiKeyOptions.Value;
    }

    public async Task<Paged<ExternalMovie>> GetMoviesByPageAsync(MoviesBy moviesBy, int pageNumber)
    {
        var response = await _httpClient.GetAsync(
            UriForMoviesWithKey(pageNumber, moviesBy));

        var movies = JsonSerializer.Deserialize<TMDBMovies>(
            await response.Content.ReadAsStringAsync(),
            _jsonSerializerOptions);

        if (movies is null || movies.Results is null) return new([]);

        return new(movies.Results.Select(
            x => new ExternalMovie(x.Id, x.Title, x.Overview, x.Release_date, x.Poster_path, [.. x.Genre_ids])).ToList(),
            movies.Page,
            movies.Page < 500,
            movies.Page > 1,
            movies.Total_Results);
    }

    private string UriForMoviesWithKey(int pageNumber, MoviesBy? getBy = MoviesBy.now_playing) => $"/3/movie/{getBy}?page={pageNumber}&api_key={_apiKeys.TMDBKey}";
}