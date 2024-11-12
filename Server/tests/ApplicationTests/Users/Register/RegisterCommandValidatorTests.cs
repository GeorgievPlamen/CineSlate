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
    public void Validator_Should_Pass_When_Valid_Command()
    {
        // Arrange
        var command = new RegisterCommand(FirstName,LastName,Email,Password);

        // Act
        var result = _sut.TestValidate(command);
    
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_Should_Fail_When_FirstName_ExceedsMaximumLength()
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
    public void Validator_Should_Fail_When_LastName_ExceedsMaximumLength()
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
    public void Validator_Should_Fail_When_Password_DoesNotMeetCriteria()
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