using Domain.Common.Models;
using FluentAssertions;
using TestUtilities.Fakers;

namespace DomainTests.Common.Models;

public class EntityTests
{
    [Fact]
    public void Entities_WithSameId_ShouldBeEqual()
    {
        // Arrange
        var user1 = UserFaker.GenerateValid();

        // Act & Assert
        user1.Should().Be(user1); // Uses Equals
    }

    [Fact]
    public void Entities_WithDifferentIds_ShouldNotBeEqual()
    {
        // Arrange
        var user1 = UserFaker.GenerateValid();
        var user2 = UserFaker.GenerateValid();

        // Act & Assert
        user1.Should().NotBe(user2); // Uses Equals
        (user1 == user2).Should().BeFalse(); // Uses operator ==
        (user1 != user2).Should().BeTrue(); // Uses operator !=
    }

    [Fact]
    public void Entity_ShouldHaveConsistentHashCodeBasedOnId()
    {
        // Arrange
        var user = UserFaker.GenerateValid();

        // Act
        // Assert
        user.GetHashCode().Should().Be(user.Id.GetHashCode());
    }

    [Fact]
    public void Entity_ShouldNotEqualNull()
    {
        // Arrange
        var user = UserFaker.GenerateValid();

        // Act & Assert
        user.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void Entities_WithDifferentTypes_ShouldNotBeEqual()
    {
        // Arrange
        var user = UserFaker.GenerateValid();
        var anotherEntity = new TestEntity(Guid.NewGuid());

        // Act & Assert
        user.Equals(anotherEntity).Should().BeFalse();
    }

    private class TestEntity : Entity<Guid>
    {
        public TestEntity(Guid id) : base(id) { }
    }
}
