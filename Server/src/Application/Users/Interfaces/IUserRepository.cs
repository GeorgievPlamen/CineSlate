namespace Application.Users.Interfaces;

public interface IUsersRepository
{
    List<Domain.Users.User> GetUsers();
}