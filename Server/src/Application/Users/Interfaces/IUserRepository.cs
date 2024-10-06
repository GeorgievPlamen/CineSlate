using Domain.Users;

namespace Application.Users.Interfaces;

public interface IUsersRepository
{
    Task<List<User>> GetUsersAsync();
    Task<bool> AddUserAsync(User user);
}