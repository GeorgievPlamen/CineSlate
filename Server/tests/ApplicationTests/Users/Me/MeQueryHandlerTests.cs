using System.Security.Claims;
using Application.Users.Interfaces;
using Application.Users.Me;
using Domain.Users;
using Domain.Users.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace ApplicationTests.Users.Me;

public class MeQueryHandlerTests
{
    private readonly MeQueryHandler _sut;
    private readonly IHttpContextAccessor _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly User _user = User.Create("John","Doe","john.doe@test.com","password.hash",Roles.User);


    public MeQueryHandlerTests()
    {
        var claims = new List<Claim> { new (ClaimTypes.Email, "john.doe@test.com") };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        
        var httpContext = new DefaultHttpContext
        {
            User = claimsPrincipal
        };

        _httpContextAccessor.HttpContext.Returns(httpContext);
        _sut = new(_httpContextAccessor,_userRepository);
    }

    [Fact]
    public async Task Handler_ShouldReturnSuccess_WhenValidParameters()
    {
        // Arrange
        _userRepository.GetUserAsync(Arg.Any<string>(),Arg.Any<CancellationToken>())
            .Returns(_user);

        // Act
        var result = await _sut.Handle(new MeQuery(),CancellationToken.None);
    
        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handler_ShouldReturnFailure_WhenUserNotFound()
    {
        // Arrange
        _userRepository.GetUserAsync(Arg.Any<string>(),Arg.Any<CancellationToken>())
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(new MeQuery(),CancellationToken.None);
    
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().NotBeEmpty();
    }
}