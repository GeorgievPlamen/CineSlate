using Application.Users.Interfaces;
using Domain.Users;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(CineSlateContext dbContext) : IUserRepository
{
    private readonly CineSlateContext _dbContext = dbContext;

    public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(user, cancellationToken);
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<User>> GetManyAsync(CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public async Task<User?> GetAsync(string email, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}