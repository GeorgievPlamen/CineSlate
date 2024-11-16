using Domain.Common.Models;
using Domain.Users;
using FluentAssertions;

namespace DomainTests.Common.Models;

public class ValueObjectTests
{
    [Fact]
    public void ValueObjects_WithSameProperties_ShouldBeEqual()
    {
        // Arrange
        var name1 = new Name("John", "Doe");
        var name2 = new Name("John", "Doe");

        // Act & Assert
        name1.Should().Be(name2); // Uses Equals method
        (name1 == name2).Should().BeTrue(); // Uses operator ==
        (name1 != name2).Should().BeFalse(); // Uses operator !=
    }

    [Fact]
    public void ValueObjects_WithDifferentProperties_ShouldNotBeEqual()
    {
        // Arrange
        var name1 = new Name("John", "Doe");
        var name2 = new Name("Jane", "Smith");

        // Act & Assert
        name1.Should().NotBe(name2); // Uses Equals method
        (name1 == name2).Should().BeFalse(); // Uses operator ==
        (name1 != name2).Should().BeTrue(); // Uses operator !=
    }

    [Fact]
    public void ValueObject_ShouldHaveConsistentHashCode()
    {
        // Arrange
        var name1 = new Name("John", "Doe");
        var name2 = new Name("John", "Doe");

        // Act & Assert
        name1.GetHashCode().Should().Be(name2.GetHashCode());
    }

    [Fact]
    public void ValueObjects_WithDifferentProperties_ShouldHaveDifferentHashCodes()
    {
        // Arrange
        var name1 = new Name("John", "Doe");
        var name2 = new Name("Jane", "Smith");

        // Act & Assert
        name1.GetHashCode().Should().NotBe(name2.GetHashCode());
    }

    [Fact]
    public void ValueObject_ShouldNotEqualNull()
    {
        // Arrange
        var name = new Name("John", "Doe");

        // Act & Assert
        name.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void ValueObjects_OfDifferentTypes_ShouldNotBeEqual()
    {
        // Arrange
        var name = new Name("John", "Doe");
        var otherValueObject = new TestValueObject();

        // Act & Assert
        name.Equals(otherValueObject).Should().BeFalse();
    }

    private class TestValueObject : ValueObject
    {
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return "Test";
        }
    }
}
