using Application.Users.Interfaces;
using Domain.Users;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsersRepository(CineSlateContext dbContext) : IUsersRepository
{
    // private readonly List<User> _users = [
    //     new() { Email = "JohnDoe@test.com", FirstName = "John", LastName = "Doe", Password = "Johns3cret!" },
    //     new() { Email = "JaneDane@test.com", FirstName = "Jane", LastName = "Dane", Password = "Janes3cret!" }];
    private readonly CineSlateContext _dbContext = dbContext;

    public async Task AddUserAsync(User user)
    {
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }
}