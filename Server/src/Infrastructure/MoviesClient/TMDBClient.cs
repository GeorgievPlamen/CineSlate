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

    public TMDBClient(IHttpClientFactory httpClientFactory, IOptions<ApiKeys> apiKeyOptions)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(TMDBurl);
        _apiKeys = apiKeyOptions.Value;
    }

    public async Task<Paged<ExternalMovie>> GetPopularMoviesByPage(int pageNumber)
    {
        var response = await _httpClient.GetAsync(UriWithApiKey($"/movie/popular?language=en-US&page={pageNumber}"));
        var popularMovies = JsonSerializer.Deserialize<TMDBPopularMovies>(
            await response.Content.ReadAsStringAsync(),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        if (popularMovies is null) return new([]);

        return new(popularMovies.Results.Select(
            x => new ExternalMovie(x.Id, x.Title, x.Overview, x.Release_date, x.Poster_path, [.. x.Genre_ids])).ToList(),
            popularMovies.Page,
            popularMovies.Page < 500,
            popularMovies.Page > 1,
            popularMovies.Total_Results);
    }

    private string UriWithApiKey(string requestPath) => $"/3{requestPath}&api_key={_apiKeys.TMDBKey}";
}
