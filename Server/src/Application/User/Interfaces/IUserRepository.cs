namespace Application.User.Interfaces;

public interface IUserRepository
{
    List<Domain.User.User> GetUsers();
}