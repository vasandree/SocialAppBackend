using Shared.Domain;

namespace Shared.Tests.Entities;

public class CreatableEntityTests
{
    private sealed class TestCreatableEntity : CreatableEntity
    {
        public TestCreatableEntity(Guid creatorId)
        {
            CreatorId = creatorId;
        }
    }

    [Fact]
    public void IsUserCreator_ReturnsTrue_WhenUserIdEqualsCreatorId()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var entity = new TestCreatableEntity(creatorId);

        // Act
        var result = entity.IsUserCreator(creatorId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsUserCreator_ReturnsFalse_WhenUserIdDiffersFromCreatorId()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var otherUserId = Guid.NewGuid();
        var entity = new TestCreatableEntity(creatorId);

        // Act
        var result = entity.IsUserCreator(otherUserId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsUserCreator_ReturnsFalse_WhenUserIdIsEmpty_AndCreatorIsNotEmpty()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var entity = new TestCreatableEntity(creatorId);

        // Act
        var result = entity.IsUserCreator(Guid.Empty);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsUserCreator_ReturnsTrue_WhenBothCreatorIdAndUserIdAreEmpty()
    {
        // Arrange
        var entity = new TestCreatableEntity(Guid.Empty);

        // Act
        var result = entity.IsUserCreator(Guid.Empty);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DifferentEntities_HaveIndependentCreatorIds()
    {
        // Arrange
        var creator1 = Guid.NewGuid();
        var creator2 = Guid.NewGuid();

        var entity1 = new TestCreatableEntity(creator1);
        var entity2 = new TestCreatableEntity(creator2);

        // Act + Assert
        Assert.True(entity1.IsUserCreator(creator1));
        Assert.False(entity1.IsUserCreator(creator2));

        Assert.True(entity2.IsUserCreator(creator2));
        Assert.False(entity2.IsUserCreator(creator1));
    }
}