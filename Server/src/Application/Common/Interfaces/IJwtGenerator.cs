namespace Application.Common.Interfaces;

public interface IJwtGenerator
{
    string GetToken(Guid userId, string firstName, string lastName);
}