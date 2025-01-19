using Infrastructure.Database.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenModel>
{
    public void Configure(EntityTypeBuilder<RefreshTokenModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value)
            .HasMaxLength(200);

        builder.HasIndex(x => x.Value)
            .IsUnique();

        builder.Property(x => x.CreatedBy)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.UpdatedBy)
            .IsRequired()
            .HasMaxLength(200);
    }
}
