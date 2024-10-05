using Domain.Users;

namespace Application.Users.Interfaces;

public interface IUsersRepository
{
    Task<List<User>> GetUsersAsync();
    Task AddUserAsync(User user);
}