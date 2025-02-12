using Application.Common;

using Domain.Users;
using Domain.Users.ValueObjects;

namespace Application.Users.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetManyByIdAsync(IEnumerable<UserId> userIds, CancellationToken cancellationToken);
    Task<User?> GetAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken);
    Task<bool> CreateAsync(User user, CancellationToken cancellationToken);
    Task<Paged<User>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(User user, CancellationToken cancellationToken);
    Task<bool> CreateRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
}