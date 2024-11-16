using System.Text.Json;
using Api.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTests.Common;

public class ExceptionHandlerTests
{
    [Fact]
    public async Task ExceptionHandler_ShouldReturnProblemDetails_WhenExceptionOccurs()
    {
        // Arrange
        var mockHttpContext = new DefaultHttpContext();
        mockHttpContext.Response.Body = new MemoryStream();

        var exception = new InvalidOperationException("Test exception");
        var exceptionHandler = new ExceptionHandler();

        // Act
        var result = await exceptionHandler.TryHandleAsync(
            mockHttpContext,
            exception,
            CancellationToken.None
        );

        // Assert
        result.Should().BeTrue();
        mockHttpContext.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

        mockHttpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(mockHttpContext.Response.Body).ReadToEndAsync();

        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseBody);

        problemDetails.Should().NotBeNull();
        problemDetails!.Status.Should().Be(StatusCodes.Status500InternalServerError);
        problemDetails.Title.Should().Be("Server error");
    }
}
