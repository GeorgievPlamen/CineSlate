using System.Net;
using System.Net.Http.Json;
using Api.Features.Users.Requests;
using TestUtilities;
using TestUtilities.Fakers;

namespace ApiTests.Features.Users;

public class UsersEndpointTests(ApiFactory api) : IClassFixture<ApiFactory>
{
    private readonly HttpClient _httpClient = api.CreateClient();

    [Fact]
    public async Task Register_ShouldReturnOk201_WhenValid()
    {
        // Arrange
        var request = new RegisterRequest("John", "Doe", "john.doe@example.com", "Password123!");

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/users/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenAlreadyRegistered()
    {
        // Arrange
        var user = UserFaker.GenerateValid();
        var request = new RegisterRequest(user.Name.First, user.Name.Last, user.Email, "Password123!");

        await api.SeedDatabaseAsync([user]);

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/users/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_ShouldReturnOk200_WhenValid()
    {
        // Arrange
        var user = UserFaker.GenerateValid();
        var request = new LoginRequest(user.Email, Constants.ValidPassword);

        await api.SeedDatabaseAsync([user]);

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/users/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Login_ShouldReturnBadRequest_WhenInvalidPassword()
    {
        // Arrange
        var user = UserFaker.GenerateValid();
        var request = new LoginRequest(user.Email, "invalidpassword");

        await api.SeedDatabaseAsync([user]);

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/users/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}