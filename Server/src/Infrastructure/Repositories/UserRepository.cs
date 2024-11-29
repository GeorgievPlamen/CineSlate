using Application.Users.Interfaces;
using Domain.Users;
using Infrastructure.Database;
using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(CineSlateContext dbContext) : IUserRepository
{
    public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(ToModel(user), cancellationToken);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<User>> GetManyAsync(CancellationToken cancellationToken)
        => await dbContext.Users
            .AsNoTracking()
            .Select(u => FromModel(u))
            .ToListAsync(cancellationToken);

    public async Task<User?> GetAsync(string email, CancellationToken cancellationToken)
        => await dbContext.Users
            .AsNoTracking()
            .Where(u => u.Email == email)
            .Select(m => FromModel(m))
            .FirstOrDefaultAsync(cancellationToken);

    private static UserModel ToModel(User user)
        => new()
        {
            Id = user.Id.Value,
            Name = user.Name,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Roles = user.Role
        };

    private static User FromModel(UserModel model)
        => User.Create(model.Name.First, model.Name.Last, model.Email, model.PasswordHash, model.Roles);
}