using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class CineSlateContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CineSlateContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}