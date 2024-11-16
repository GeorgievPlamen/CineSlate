using Api.Common;
using Application.Common;
using Domain.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ApiTests.Common;

public class ResponseTests
{
    [Fact]
    public void Response_ShouldReturnOkWithValue_WhenResultIsSuccess()
    {
        // Arrange
        var testResult = Result<string>.Success("expected value");

        // Act
        var response = Response<string>.Match(testResult);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<Ok<string>>();

        var okResult = response as Ok<string>;
        okResult.Should().NotBeNull();
        okResult?.Value.Should().Be("expected value");
    }

    [Fact]
    public void Response_ShouldReturn500_WhenResultIsFailureServerError()
    {
        // Arrange
        var testResult = Result<int>.Failure(Error.ServerError());

        // Act
        var response = Response<int>.Match(testResult);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<ProblemHttpResult>();

        var problemResult = response as ProblemHttpResult;
        problemResult.Should().NotBeNull();
        problemResult?.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public void Response_ShouldReturn400_WhenResultIsFailureBadRequest()
    {
        // Arrange
        var testResult = Result<int>.Failure(Error.BadRequest());

        // Act
        var response = Response<int>.Match(testResult);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<ProblemHttpResult>();

        var problemResult = response as ProblemHttpResult;
        problemResult.Should().NotBeNull();
        problemResult?.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public void Response_ShouldReturnValidationProblem_WhenResultIsFailureValidation()
    {
        // Arrange
        var testResult = Result<int>.Failure(Error.Validation(
            "SomeValidationProblem", "Details of the validation problem"));

        // Act
        var response = Response<int>.Match(testResult);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<ValidationProblem>();

        var problemResult = response as ValidationProblem;
        problemResult.Should().NotBeNull();
        problemResult?.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public void Response_ShouldReturn404_WhenResultIsFailureNotFound()
    {
        // Arrange
        var testResult = Result<int>.Failure(Error.NotFound());

        // Act
        var response = Response<int>.Match(testResult);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<ProblemHttpResult>();

        var problemResult = response as ProblemHttpResult;
        problemResult.Should().NotBeNull();
        problemResult?.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}