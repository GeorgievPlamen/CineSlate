using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Common.Models;
using Domain.Movies;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class CineSlateContext(
    DbContextOptions options,
    IHttpContextAccessor httpContextAccessor) : DbContext(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<MovieAggregate> Movies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CineSlateContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        List<DomainEvent> domainEvents = [];

        foreach (var entry in ChangeTracker.Entries<IEntity>())
        {
            if (entry.Entity.DomainEvents.Count > 0)
                domainEvents.AddRange(entry.Entity.DomainEvents);

            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(
                ClaimTypes.Email)?.Value ?? "Could not get email. User Not logged in.";

            entry.Entity.SetUpdated(email, DateTimeOffset.UtcNow);

            if (entry.State == EntityState.Added)
                entry.Entity.SetCreated(email, DateTimeOffset.UtcNow);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}