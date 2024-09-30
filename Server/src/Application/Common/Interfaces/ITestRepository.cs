using Domain.Test;

namespace Application.Common.Interfaces;

public interface ITestRepository
{
    List<TestEntity> GetAll();
}