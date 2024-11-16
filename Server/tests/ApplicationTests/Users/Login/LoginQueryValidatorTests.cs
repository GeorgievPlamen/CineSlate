using Application.Users.Login;
using FluentValidation.TestHelper;

namespace ApplicationTests.Users.Login;

public class LoginQueryValidatorTests
{
    private readonly LoginQueryValidator _sut = new();
    private readonly string password = "fakepassword";

    [Fact]
    public void Validator_ShouldPass_WhenValid()
    {
        // Arrange
        var command = new LoginCommand("john@test.com", password);

        // Act
        var result = _sut.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_ShouldFail_WhenEmailIsNotValid()
    {
        // Arrange
        var command = new LoginCommand("invalidEmail", password);

        // Act
        var result = _sut.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
}