using System.Net.Http.Json;
using System.Text.Json;

using Api.Features.Users.Requests;

using Application.Users.Login;

using Infrastructure.Repositories.MappingExtensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;

using TestUtilities;
using TestUtilities.Fakers;

namespace ApiTests.Common;

public abstract class AuthenticatedTest(ApiFactory factory) : IClassFixture<ApiFactory>
{
    protected readonly ApiFactory Api = factory;
    protected readonly HttpClient Client = factory.CreateClient();
    private string _authToken = null!;

    protected async Task<Guid> AuthenticateAsync()
    {
        Guid userId = Guid.Empty;

        if (_authToken == null)
        {
            var user = UserFaker.GenerateValid();
            var request = new LoginRequest(user.Email, Constants.ValidPassword);

            await Api.SeedDatabaseAsync([user.ToModel()]);

            var response = await Client.PostAsJsonAsync("/api/users/login", request);

            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            userId = loginResponse?.Id ?? Guid.Empty;

            _authToken = loginResponse?.Token ?? "Could not get token";
        }

        Client.DefaultRequestHeaders.Authorization = new(JwtBearerDefaults.AuthenticationScheme, _authToken);

        return userId;
    }
}