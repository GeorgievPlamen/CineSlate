using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id)
            .HasConversion(id => id.Value, value=> new(value));

        builder
            .HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.UpdatedBy)
            .IsRequired()
            .HasMaxLength(100);

        var name = builder.ComplexProperty(u => u.Name);

        name.Property(n => n!.First)
            .IsRequired()
            .HasMaxLength(50);

        name.Property(n => n!.Last)
            .IsRequired()
            .HasMaxLength(50);
    }
}