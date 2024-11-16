using Application.Users.Interfaces;
using Application.Users.Register;
using Domain.Users;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using TestUtilities.Fakers;

namespace ApplicationTests.Users.Register;

public class RegisterCommandHandlerTests
{
    private readonly RegisterCommandHandler _sut;
    private readonly RegisterCommand _command;
    private readonly IUserIdentity _userIdentity = Substitute.For<IUserIdentity>();
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly User _user = UserFaker.GenerateValid();

    public RegisterCommandHandlerTests()
    {
        _sut = new(_userIdentity, _userRepository);
        _command = new(_user.Name.First, _user.Name.Last, _user.Email, _user.PasswordHash);
    }

    [Fact]
    public async Task Handler_ShouldReturnSuccess_WhenValid()
    {
        // Arrange
        _userRepository.GetUserAsync(
            Arg.Any<string>(), Arg.Any<CancellationToken>())
            .ReturnsNull();

        _userRepository.AddUserAsync(
            Arg.Any<User>(), Arg.Any<CancellationToken>())
            .Returns(true);

        _userIdentity.HashPassword(Arg.Any<string>()).Returns("password.hash");

        // Act
        var result = await _sut.Handle(_command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handler_ShouldReturnFailure_WhenUserAlreadyRegistered()
    {
        // Arrange
        _userRepository.GetUserAsync(
            Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(_user);

        // Act
        var result = await _sut.Handle(_command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handler_ShouldReturnFailure_WhenUserCouldNotBeAdded()
    {
        // Arrange
        _userRepository.GetUserAsync(
            Arg.Any<string>(), Arg.Any<CancellationToken>())
            .ReturnsNull();

        _userRepository.AddUserAsync(
            Arg.Any<User>(), Arg.Any<CancellationToken>())
            .Returns(false);

        _userIdentity.HashPassword(Arg.Any<string>()).Returns("password.hash");

        // Act
        var result = await _sut.Handle(_command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().NotBeEmpty();
    }
}