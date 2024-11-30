using Application.Users.Interfaces;
using Domain.Users;
using Infrastructure.Database;
using Infrastructure.Repositories.MappingExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(CineSlateContext dbContext) : IUserRepository
{
    public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(user.ToModel(), cancellationToken);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<User>> GetManyAsync(CancellationToken cancellationToken)
        => await dbContext.Users
            .AsNoTracking()
            .Select(u => u.Unwrap())
            .ToListAsync(cancellationToken);

    public async Task<User?> GetAsync(string email, CancellationToken cancellationToken)
        => await dbContext.Users
            .AsNoTracking()
            .Where(u => u.Email == email)
            .Select(m => m.Unwrap())
            .FirstOrDefaultAsync(cancellationToken);
}