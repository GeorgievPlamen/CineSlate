using Domain.Users;

namespace Application.Users.Interfaces;

public interface IUsersRepository
{
    Task<List<User>> GetUsersAsync();
    Task<User?> GetUserAsync(string email);
    Task<bool> AddUserAsync(User user);
}