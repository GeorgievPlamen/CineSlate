using System.IdentityModel.Tokens.Jwt;
using Domain.Common.Models;
using Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class CineSlateContext(
    DbContextOptions options,
    IHttpContextAccessor httpContextAccessor) : DbContext(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CineSlateContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<IEntity>())
        {
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(
                JwtRegisteredClaimNames.Email)?.Value ??
                "Could not get email. User Not logged in.";

            entry.Entity.SetUpdated(email, DateTimeOffset.UtcNow);

            if (entry.State == EntityState.Added)
            {
                entry.Entity.SetCreated(email, DateTimeOffset.UtcNow);
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}