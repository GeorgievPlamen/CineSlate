using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class UsersConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(20)
            .HasConversion<string>();

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.CreatedBy)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.UpdatedBy)
            .IsRequired()
            .HasMaxLength(200);

        var name = builder.ComplexProperty(u => u.Name);

        name.Property(n => n!.First)
            .IsRequired()
            .HasMaxLength(50);

        name.Property(n => n!.Last)
            .IsRequired()
            .HasMaxLength(50);
    }
}