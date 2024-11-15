using TestUtilities;

namespace ApiTests.Features.Users;

public class UsersRegisterTest(ApiFactory api) : IClassFixture<ApiFactory>
{
    private readonly HttpClient _httpClient = api.CreateClient();

    [Fact]
    public void TestName()
    {
        // Arrange
    
        // Act
    
        // Assert
    }
}