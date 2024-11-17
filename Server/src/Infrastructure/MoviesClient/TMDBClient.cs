using System.Text.Json;
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

    public async Task GetPopularMoviesByPage(int pageNumber)
    {
        var response = await _httpClient.GetAsync(UriWithApiKey($"/movie/popular?language=en-US&page={pageNumber}"));
        var json = await JsonSerializer.DeserializeAsync<TMDBMovie>(await response.Content.ReadAsStreamAsync());
    }

    private string UriWithApiKey(string requestPath) => $"/3{requestPath}&api_key={_apiKeys.TMDBKey}";
}
