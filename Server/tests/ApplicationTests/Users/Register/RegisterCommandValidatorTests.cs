using Application.Users.Register;
using Domain.Users;
using FluentValidation.TestHelper;
using TestUtilities.Fakers;

namespace ApplicationTests.Users.Register;

public class RegisterCommandValidatorTests
{
    private readonly RegisterCommandValidator _sut = new();
    private readonly User _user = UserFaker.GenerateValid();
    private const string ValidPassword = "Password123!";

    [Fact]
    public void Validate_ShouldPass_WhenValidCommand()
    {
        // Arrange
        var command = new RegisterCommand(_user.Name.First, _user.Name.Last, _user.Email, ValidPassword);

        // Act
        var result = _sut.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldFail_WhenFirstNameExceedsMaximumLength()
    {
        // Arrange
        var command = new RegisterCommand(
            "JohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohn"
            , _user.Name.Last, _user.Email, ValidPassword);

        // Act
        var result = _sut.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenLastNameExceedsMaximumLength()
    {
        // Arrange
        var command = new RegisterCommand(
            _user.Name.First,
            "JohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohn"
            , _user.Email, ValidPassword);

        // Act
        var result = _sut.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenPasswordDoesNotMeetCriteria()
    {
        // Arrange
        var command = new RegisterCommand(_user.Name.First, _user.Name.Last, _user.Email, "simplepassword");

        // Act
        var result = _sut.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
}