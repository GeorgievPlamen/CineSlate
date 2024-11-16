using FluentAssertions;
using Xunit;

namespace TestUtilities;

public class FakeTest
{
    [Fact]
    public void ThisTestIsForRemovingTheNoTestInProjectWarning()
    {
        // Arrange
        var one = 1;
        var two = 2;
        
        // Act
        var result = two > one;

        // Assert
        result.Should().BeTrue();
    }
}