using Domain.Users.ValueObjects;

namespace Application.Common.Interfaces;

public interface IAppContext
{
    public UserId GetUserId();
}