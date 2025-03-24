using Infrastructure.Database.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<CommentModel>
{
    public void Configure(EntityTypeBuilder<CommentModel> builder)
    {
        builder.HasIndex(x => x.UserId);

        builder.Property(m => m.CreatedBy)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.UpdatedBy)
            .IsRequired()
            .HasMaxLength(200);

        var name = builder.ComplexProperty(u => u.Username);

        name.Property(n => n!.Value)
            .IsRequired()
            .HasMaxLength(110);

        name.Property(n => n!.OnlyName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Comment)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Version)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .IsRowVersion();
    }
}