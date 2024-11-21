using Domain.Users;

namespace Application.Users.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetManyAsync(CancellationToken cancellationToken);
    Task<User?> GetAsync(string email, CancellationToken cancellationToken);
    Task<bool> CreateAsync(User user, CancellationToken cancellationToken);
}