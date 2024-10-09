using Application.Users.Interfaces;
using Domain.Users;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsersRepository(CineSlateContext dbContext) : IUsersRepository
{
    private readonly CineSlateContext _dbContext = dbContext;

    public async Task<bool> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(user, cancellationToken);
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<User>> GetUsersAsync(CancellationToken cancellationToken)
        => await _dbContext.Users.ToListAsync(cancellationToken);

    public async Task<User?> GetUserAsync(string email, CancellationToken cancellationToken)
        => await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
}