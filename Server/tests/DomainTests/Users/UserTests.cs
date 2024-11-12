using Domain.Users;
using Domain.Users.Enums;
using FluentAssertions;

namespace DomainTests.Users;

public class UserTests
{
    [Fact]
    public void Create_ShouldCreateUser_WhenValidParams()
    {
        // Act
        var user = User.Create("Test","Test","Test","Test",Roles.User);
    
        // Assert
        user.Should().NotBeNull();
    }
}