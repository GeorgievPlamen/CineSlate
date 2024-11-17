using System.Text.Json;
using Application.Movies.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Infrastructure.MoviesClient;

public class TMDBClient : IMoviesClient
{
    private const string TMDBurl = "https://api.themoviedb.org/3";
    private readonly HttpClient _httpClient;

    public TMDBClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(TMDBurl);
        _httpClient.DefaultRequestHeaders.Authorization = new(JwtBearerDefaults.AuthenticationScheme,
        "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJmZmIyZmYzZWZlN2ZkMDQyODU2ZTc2MGI3OGI3MjRhMyIsIm5iZiI6MTczMTg0ODQyNS44MjI1NDkzLCJzdWIiOiI2NmY3ZTJkZjFhOWM5MTg4ZmVjYzBkZWEiLCJzY29wZXMiOlsiYXBpX3JlYWQiXSwidmVyc2lvbiI6MX0.TEp-lt1JCTJfbxXmjYrkyTYYL-NFHEQNx3vQ8OqLjwI");
    }

    public async Task GetPopularMoviesByPage(int pageNumber)
    {
        var response = await _httpClient.GetAsync("/discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc");
        var json = await JsonSerializer.DeserializeAsync<string>(await response.Content.ReadAsStreamAsync());

        System.Console.WriteLine(json);
    }
}
