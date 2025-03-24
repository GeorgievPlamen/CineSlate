using Infrastructure.Database.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class GenresConfiguration : IEntityTypeConfiguration<GenreModel>
{
    public void Configure(EntityTypeBuilder<GenreModel> builder)
    {
        builder.Property(g => g.Name)
            .HasMaxLength(100);

        builder.Property(u => u.CreatedBy)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.UpdatedBy)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Version)
            .IsRowVersion();

        builder.Property(e => e.Version)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .IsRowVersion();
    }
}
