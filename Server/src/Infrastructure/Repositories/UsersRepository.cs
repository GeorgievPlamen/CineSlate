using Application.Users.Interfaces;
using Domain.Users;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsersRepository(CineSlateContext dbContext) : IUsersRepository
{
    private readonly CineSlateContext _dbContext = dbContext;

    public async Task<bool> AddUserAsync(User user)
    {
        await _dbContext.AddAsync(user);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }
    public async Task<User?> GetUserAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}