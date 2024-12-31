using System.Security.Claims;
using Infrastructure.Database.Models;
using Infrastructure.Database.Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class CineSlateContext(
    DbContextOptions options,
    IHttpContextAccessor httpContextAccessor) : DbContext(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<MovieModel> Movies { get; set; } = null!;
    public DbSet<GenreModel> Genres { get; set; } = null!;
    public DbSet<ReviewModel> Reviews { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CineSlateContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<IModel>())
        {
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(
                ClaimTypes.Email)?.Value ?? "Could not get email. User Not logged in.";

            entry.Entity.SetUpdated(email, DateTimeOffset.UtcNow);

            if (entry.State == EntityState.Added)
                entry.Entity.SetCreated(email ?? "Unknown", DateTimeOffset.UtcNow);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}