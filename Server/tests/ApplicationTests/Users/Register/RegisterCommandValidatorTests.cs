using Application.Users.Register;
using FluentValidation.TestHelper;

namespace ApplicationTests.Users.Register;

public class RegisterCommandValidatorTests
{
    private readonly RegisterCommandValidator _sut = new();
    private const string FirstName = "John"; 
    private const string LastName = "Doe"; 
    private const string Email = "john.doe@test.com"; 
    private const string Password = "Secre7Pa$$w0rd"; 

    [Fact]
    public void Validate_ShouldPass_WhenValidCommand()
    {
        // Arrange
        var command = new RegisterCommand(FirstName,LastName,Email,Password);

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
            ,LastName,Email,Password);
        // Act

        var result = _sut.TestValidate(command);

        System.Console.WriteLine(result);
    
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenLastNameExceedsMaximumLength()
    {
        // Arrange
        var command = new RegisterCommand(
            FirstName,
            "JohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohn"
            ,Email,Password);
        // Act

        var result = _sut.TestValidate(command);

        System.Console.WriteLine(result);
    
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenPasswordDoesNotMeetCriteria()
    {
        // Arrange
        var command = new RegisterCommand(FirstName,LastName,Email,"simplepassword");
        // Act

        var result = _sut.TestValidate(command);

        System.Console.WriteLine(result);
    
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
}