namespace Domain.Test;

public class TestEntity
{
    public Guid Id { get; }
    public string Name { get; }

    public TestEntity(string name)
    {
        Id = Guid.NewGuid();
        Name = $"Testing: {name}";
    }
}