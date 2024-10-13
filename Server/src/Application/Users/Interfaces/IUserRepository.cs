using Domain.Users;

namespace Application.Users.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync(CancellationToken cancellationToken);
    Task<User?> GetUserAsync(string email, CancellationToken cancellationToken);
    Task<bool> AddUserAsync(User user, CancellationToken cancellationToken);
}