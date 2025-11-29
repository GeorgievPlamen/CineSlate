using System.Net;
using System.Net.Http.Json;

using Api.Features.Users;
using Api.Features.Users.Requests;

using ApiTests.Common;

using Infrastructure.Repositories.MappingExtensions;

using TestUtilities;
using TestUtilities.Fakers;

namespace ApiTests.Features.Users;

#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
public class UsersEndpointTests(ApiFactory factory) : AuthenticatedTest(factory)
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
    private static string TestUri(string uri) => $"{UsersEndpoint.Uri}{uri}";

    [Fact]
    public async Task Register_ShouldReturnOk201_WhenValid()
    {
        // Arrange
        var request = new RegisterRequest("John", "john.doe@example.com", "Password123!");

        // Act
        var response = await Client.PostAsJsonAsync(TestUri("/register"), request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenAlreadyRegistered()
    {
        // Arrange
        var user = UserFaker.GenerateValid();
        var request = new RegisterRequest(user.Username.OnlyName, user.Email, "Password123!");

        await factory.SeedDatabaseAsync([user.ToModel()]);

        // Act
        var response = await Client.PostAsJsonAsync(TestUri("/register"), request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_ShouldReturnOk200_WhenValid()
    {
        // Arrange
        var user = UserFaker.GenerateValid();
        var request = new LoginRequest(user.Email, Constants.ValidPassword);

        await factory.SeedDatabaseAsync([user.ToModel()]);

        // Act
        var response = await Client.PostAsJsonAsync(TestUri("/login"), request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Login_ShouldReturnBadRequest_WhenInvalidPassword()
    {
        // Arrange
        var user = UserFaker.GenerateValid();
        var request = new LoginRequest(user.Email, "invalidpassword");

        await factory.SeedDatabaseAsync([user.ToModel()]);

        // Act
        var response = await Client.PostAsJsonAsync(TestUri("/login"), request);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetManyById_ShouldReturnFoundUsers()
    {
        // Arrange
        var user = UserFaker.GenerateMany(2);
        var request = new GetUsersRequest([.. user.Select(u => u.Id.Value)]);

        await factory.SeedDatabaseAsync(user.Select(u => u.ToModel()));

        // Act
        var response = await Client.PostAsJsonAsync(TestUri("/"), request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Update_ShouldReturnOk_WhenValid()
    {
        // Arrange
        var userId = await AuthenticateAsync();

        // Act
        var response = await Client.PutAsync(TestUri($"/{userId}?bio={"testBio"}"), null);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetLatestUsers_ShouldReturnLatestUsers()
    {
        // Arrange
        var user = UserFaker.GenerateMany(2);

        await factory.SeedDatabaseAsync(user.Select(u => u.ToModel()));

        // Act
        var response = await Client.GetAsync(TestUri("/1"));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetMe_ShouldReturnUser()
    {
        // Arrange
        await AuthenticateAsync();

        // Act
        var response = await Client.GetAsync(TestUri("/me"));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostRefreshToken_ShouldReturnNewToken()
    {
        // Arrange
        await AuthenticateAsync();

        Assert.NotNull(RefreshToken);
        var request = new RefreshTokenRequest(RefreshToken);

        // Act
        var response = await Client.PostAsJsonAsync(TestUri("/refresh-token"), request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}