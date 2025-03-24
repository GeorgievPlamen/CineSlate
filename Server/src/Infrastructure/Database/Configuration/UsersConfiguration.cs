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

        builder.HasIndex(u => u.CreatedAt);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Bio)
            .HasMaxLength(200);

        builder.Property(u => u.Roles)
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

        var name = builder.ComplexProperty(u => u.Username);

        name.Property(n => n!.Value)
            .IsRequired()
            .HasMaxLength(110);

        name.Property(n => n!.OnlyName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Version)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .IsRowVersion();
    }
}