using System.Text.Json;

using Application.Common;
using Application.Movies;
using Application.Movies.Interfaces;

using Infrastructure.Common.Models;
using Infrastructure.MoviesClient;

using Microsoft.Extensions.Options;

namespace Infrastructure.MovieClient;

public class TMDBClient : IMovieClient
{
    private const string TMDBurl = "https://api.themoviedb.org/";
    private readonly HttpClient _httpClient;
    private readonly ApiKeys _apiKeys;
    private static readonly JsonSerializerOptions _jsonSerializerOptions =
        new() { PropertyNameCaseInsensitive = true };
    private string UriForMoviesWithKey(int pageNumber, MoviesBy? getBy = MoviesBy.now_playing)
        => $"/3/movie/{getBy}?page={pageNumber}&api_key={_apiKeys.TMDBKey}";

    private string UriForMovieDetailsWithKey(int id)
        => $"/3/movie/{id}?api_key={_apiKeys.TMDBKey}";
    private string UriForMovieSearchWithKey(string searchCriteria, int pageNumber)
        => $"/3/search/movie?query={searchCriteria}?page=${pageNumber}&api_key={_apiKeys.TMDBKey}";

    public TMDBClient(IHttpClientFactory httpClientFactory, IOptions<ApiKeys> apiKeyOptions)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(TMDBurl);
        _apiKeys = apiKeyOptions.Value;
    }

    public async Task<Paged<ExternalMovie>> GetMoviesByPageAsync(MoviesBy moviesBy, int pageNumber, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(
            UriForMoviesWithKey(pageNumber, moviesBy));

        var movies = JsonSerializer.Deserialize<TMDBMovies>(
            await response.Content.ReadAsStringAsync(cancellationToken),
            _jsonSerializerOptions);

        if (movies is null || movies.Results is null) return new([]);

        return new(movies.Results.Select(
            x => new ExternalMovie(
                x.Id,
                x.Title,
                x.Overview,
                x.Release_date,
                x.Poster_path,
                x.Genre_ids)).ToList(),
            movies.Page,
            movies.Page < 500,
            movies.Page > 1,
            movies.Total_Results);
    }

    public async Task<ExternalMovieDetailed?> GetMovieDetailsAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(
            UriForMovieDetailsWithKey(id), cancellationToken);

        var movieDetailed = JsonSerializer.Deserialize<TMDBMovieDetailed>(
            await response.Content.ReadAsStringAsync(cancellationToken),
            _jsonSerializerOptions);

        if (movieDetailed is null) return null;

        return new ExternalMovieDetailed(
            movieDetailed.Id,
            movieDetailed.Title,
            movieDetailed.Overview,
            movieDetailed.Release_date,
            movieDetailed.Poster_path,
            movieDetailed.Genres,
            movieDetailed.Backdrop_path,
            movieDetailed.Budget,
            movieDetailed.Homepage,
            movieDetailed.Imdb_id,
            movieDetailed.Origin_country.FirstOrDefault() ?? "",
            movieDetailed.Revenue,
            movieDetailed.Runtime,
            movieDetailed.Status,
            movieDetailed.Tagline);
    }

    public async Task<Paged<ExternalMovie>> GetMoviesByTitle(string searchCriteria, int pageNumber, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(UriForMovieSearchWithKey(searchCriteria, pageNumber));

        var movies = JsonSerializer.Deserialize<TMDBMovies>( // TODO
            await response.Content.ReadAsStringAsync(cancellationToken),
            _jsonSerializerOptions);

        if (movies is null || movies.Results is null) return new([]);

        return new(movies.Results.Select(
            x => new ExternalMovie(
                x.Id,
                x.Title,
                x.Overview,
                x.Release_date,
                x.Poster_path,
                x.Genre_ids)).ToList(),
            movies.Page,
            movies.Page < 500,
            movies.Page > 1,
            movies.Total_Results);
    }
}