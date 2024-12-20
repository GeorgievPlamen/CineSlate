using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Users.ValueObjects;
using FluentAssertions;
using Infrastructure.Common;
using Infrastructure.Common.Models;
using Microsoft.Extensions.Options;

namespace InfrastructureTests.Common;

public class UserIdentityTests
{
    private readonly DateTimeProvider _dateTimeProvider = new();
    private readonly JwtSettings _jwtSettings = new()
    {
        Secret = "supersecretkey1234567890needstobelonger256bits",
        Issuer = "testIssuer",
        Audience = "testAudience",
        ExpiryMinutes = 60
    };

    private readonly UserIdentity _sut;

    public UserIdentityTests()
    {
        _sut = new(_dateTimeProvider, Options.Create(_jwtSettings));
    }

    [Fact]
    public void GenerateJwtToken_ShouldReturnValidJwtToken()
    {
        // Act
        var token = _sut.GenerateJwtToken(
            UserId.Create(),
            Username.Create("John", UserId.Create()),
            "john.doe@example.com",
            "Admin");

        // Assert
        token.Should().NotBeNullOrWhiteSpace();

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        jwtToken.Issuer.Should().Be(_jwtSettings.Issuer);
        jwtToken.Audiences.Should().Contain(_jwtSettings.Audience);
    }


    [Fact]
    public void HashPassword_ShouldReturnValidHash()
    {
        // Arrange
        var userIdentity = new UserIdentity(_dateTimeProvider, Options.Create(new JwtSettings()));

        // Act
        var hashedPassword = userIdentity.HashPassword("password123");

        // Assert
        hashedPassword.Should().NotBeNullOrWhiteSpace();
        hashedPassword.Split('.').Length.Should().Be(2); // Ensure salt and hash parts
    }

    [Theory]
    [InlineData("password123", true)]
    [InlineData("wrongpassword", false)]
    public void ValidatePassword_ShouldValidateCorrectly(string inputPassword, bool expectedResult)
    {
        // Arrange
        var userIdentity = new UserIdentity(_dateTimeProvider, Options.Create(new JwtSettings()));
        var storedPassword = userIdentity.HashPassword("password123");

        // Act
        var isValid = userIdentity.ValidatePassword(inputPassword, storedPassword);

        // Assert
        isValid.Should().Be(expectedResult);
    }

    [Fact]
    public void ValidatePassword_ShouldReturnFalseForMalformedStoredPassword()
    {
        // Arrange
        var userIdentity = new UserIdentity(_dateTimeProvider, Options.Create(new JwtSettings()));

        // Act
        var isValid = userIdentity.ValidatePassword("password123", "malformedPassword");

        // Assert
        isValid.Should().BeFalse();
    }
}