using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Application.Users.Interfaces;
using Domain.Users;
using Domain.Users.ValueObjects;
using Infrastructure.Database;
using Infrastructure.Repositories.MappingExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(CineSlateContext dbContext) : IUserRepository
{
    public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
    {
        dbContext.Add(user.ToModel());
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<User>> GetManyByIdAsync(IEnumerable<UserId> userIds, CancellationToken cancellationToken)
        => await dbContext.Users
            .AsNoTracking()
            .Where(u => userIds.Select(x => x.Value).Contains(u.Id))
            .Select(u => u.Unwrap())
            .ToListAsync(cancellationToken);

    public async Task<User?> GetAsync(string email, CancellationToken cancellationToken)
        => await dbContext.Users
            .AsNoTracking()
            .Where(u => u.Email == email)
            .Select(m => m.Unwrap())
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => await dbContext.Users
            .AsNoTracking()
            .Where(u => u.Email == email)
            .Select(u => u.Unwrap())
            .FirstOrDefaultAsync(cancellationToken);
}