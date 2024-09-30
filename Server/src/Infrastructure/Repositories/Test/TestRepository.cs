using Application.Common.Interfaces;
using Domain.Test;

namespace Infrastructure.Repositories.Test;

public class TestRepository : ITestRepository
{
    private readonly List<TestEntity> testEntities = [new("John"), new("Greg"), new("Eva")];
    public List<TestEntity> GetAll()
    {
        return testEntities.ToList();
    }
}