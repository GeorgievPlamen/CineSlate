using Domain.Common.Models;
using Domain.Users.ValueObjects;
using FluentAssertions;

namespace DomainTests.Common.Models;

public class ValueObjectTests
{
    [Fact]
    public void ValueObjects_WithSameProperties_ShouldBeEqual()
    {
        // Arrange
        var userId = UserId.Create();
        var name1 = Username.Create("John", userId);
        var name2 = Username.Create("John", userId);


        // Act & Assert
        name1.Should().Be(name2); // Uses Equals method
        (name1 == name2).Should().BeTrue(); // Uses operator ==
        (name1 != name2).Should().BeFalse(); // Uses operator !=
    }

    [Fact]
    public void ValueObjects_WithDifferentProperties_ShouldNotBeEqual()
    {
        // Arrange
        var userId = UserId.Create();
        var name1 = Username.Create("John", userId);
        var name2 = Username.Create("Jane", userId);

        // Act & Assert
        name1.Should().NotBe(name2); // Uses Equals method
        (name1 == name2).Should().BeFalse(); // Uses operator ==
        (name1 != name2).Should().BeTrue(); // Uses operator !=
    }

    [Fact]
    public void ValueObject_ShouldHaveConsistentHashCode()
    {
        // Arrange
        var userId = UserId.Create();
        var name1 = Username.Create("John", userId);
        var name2 = Username.Create("John", userId);

        // Act & Assert
        name1.GetHashCode().Should().Be(name2.GetHashCode());
    }

    [Fact]
    public void ValueObjects_WithDifferentProperties_ShouldHaveDifferentHashCodes()
    {
        // Arrange
        var userId = UserId.Create();
        var name1 = Username.Create("John", userId);
        var name2 = Username.Create("Jane", userId);

        // Act & Assert
        name1.GetHashCode().Should().NotBe(name2.GetHashCode());
    }

    [Fact]
    public void ValueObject_ShouldNotEqualNull()
    {
        // Arrange
        var userId = UserId.Create();
        var name = Username.Create("John", userId);

        // Act & Assert
        name.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void ValueObjects_OfDifferentTypes_ShouldNotBeEqual()
    {
        // Arrange
        var userId = UserId.Create();
        var name = Username.Create("John", userId);
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
