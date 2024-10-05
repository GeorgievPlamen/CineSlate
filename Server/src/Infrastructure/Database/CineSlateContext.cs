using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class CineSlateContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}