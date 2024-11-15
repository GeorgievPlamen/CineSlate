using System.Net;
using System.Net.Http.Json;
using TestUtilities;

namespace ApiTests.Features.Users;

public class UsersRegisterTest(ApiFactory api) : IClassFixture<ApiFactory>
{
    private readonly HttpClient _httpClient = api.CreateClient();

    [Fact]
    public async Task Register_ShouldReturnOk201_WhenValid()
    {
        var request = new
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "Password123!"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/users/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}