using Domain.Users;
using Domain.Users.Enums;

namespace DomainTests.Users;

public class UserTests
{
    [Fact]
    public void CreateUser_Returns_User_When_Valid()
    {
        // Arrange
        // Act
        var user = User.Create("Test","Test","Test","Test",Roles.User);
    
        // Assert
        System.Console.WriteLine(user.ToString());
        Assert.NotNull(user);
    }
}