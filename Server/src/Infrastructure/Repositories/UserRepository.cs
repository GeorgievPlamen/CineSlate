using Application.Common;
using Application.Users.Interfaces;

using Domain.Users;
using Domain.Users.ValueObjects;

using Infrastructure.Database;
using Infrastructure.Database.Models;
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

    public async Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken)
        => await dbContext.Users
            .AsNoTracking()
            .Where(u => u.Id == userId.Value)
            .Select(m => m.Unwrap())
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<Paged<User>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var total = await dbContext.Users.CountAsync(cancellationToken);

        var values = await dbContext.Users
            .AsNoTracking()
            .OrderBy(u => u.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(u => u.Unwrap())
            .ToListAsync(cancellationToken);

        return new Paged<User>(values, page, page * pageSize < total, page > 1, total);
    }

    public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        var savedUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id.Value, cancellationToken);
        if (savedUser is null)
            return false;

        var userModel = user.ToModel();

        savedUser.Bio = userModel.Bio;
        savedUser.AvatarBlob = userModel.AvatarBlob;
        savedUser.WatchlistId = userModel.WatchlistId;

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> CreateRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        if (refreshToken is null || refreshToken?.UserId?.Value is null) return false;

        var token = new RefreshTokenModel
        {
            Id = refreshToken.Id,
            UserId = refreshToken.UserId.Value,
            Value = refreshToken?.Value ?? "",
            ExpiresAt = refreshToken?.ExpiresAt ?? DateTime.UtcNow
        };


        dbContext.Add(token);
        var oldTokens = dbContext.RefreshTokens.Where(x => x.UserId == refreshToken!.UserId.Value);
        dbContext.RefreshTokens.RemoveRange(oldTokens);

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var token = await dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Value == refreshToken, cancellationToken);
        if (token is null)
            return null;

        return RefreshToken.Create(
            token.Id,
            UserId.Create(token.UserId),
            token.Value,
            token.ExpiresAt);
    }
}