using Application.Common;
using Application.Users.Interfaces;
using Application.Users.Login;

using Domain.Users;
using Domain.Users.Errors;
using Domain.Users.ValueObjects;

using FluentAssertions;

using NSubstitute;

using TestUtilities;
using TestUtilities.Fakers;

namespace ApplicationTests.Users.Login;

public class LoginQueryHandlerTests
{
    private readonly LoginQueryHandler _sut;
    private readonly LoginCommand _command;
    private readonly IUserIdentity _userIdentity = Substitute.For<IUserIdentity>();
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly User _user = UserFaker.GenerateValid();

    public LoginQueryHandlerTests()
    {
        _sut = new(_userRepository, _userIdentity);
        _command = new(_user.Email, Constants.ValidPassword);
    }

    [Fact]
    public async Task Handler_ShouldReturnSuccess_WhenValid()
    {
        // Arrange
        _userRepository.GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(_user);

        _userIdentity.ValidatePassword(Arg.Any<string>(), Arg.Any<string>())
            .Returns(true);

        _userIdentity.GenerateJwtToken(
            Arg.Any<UserId>(),
            Arg.Any<Username>(),
            Arg.Any<string>(),
            Arg.Any<string>())
                .Returns("fakejwt");

        _userIdentity.GenerateRefreshToken().Returns("fake-refresh-token");

        _userRepository.CreateRefreshTokenAsync(Arg.Any<RefreshToken>(), Arg.Any<CancellationToken>()).Returns(true);

        // Act
        var result = await _sut.Handle(_command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handler_ShouldReturnNotFoundError_WhenUserNotFound()
    {
        // Act
        var result = await _sut.Handle(_command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Errors[0].Code.Should().Be(UserErrors.NotFound().Code);
    }

    [Fact]
    public async Task Handler_ShouldReturnNotFoundError_WhenPasswordIsWrong()
    {
        // Arrange
        _userRepository.GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(_user);

        _userIdentity.ValidatePassword(Arg.Any<string>(), Arg.Any<string>())
            .Returns(false);

        // Act
        var result = await _sut.Handle(_command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Errors[0].Code.Should().Be(UserErrors.NotFound().Code);
    }
}