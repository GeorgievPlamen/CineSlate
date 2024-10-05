using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class CineSlateContext : DbContext
{
    public CineSlateContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}