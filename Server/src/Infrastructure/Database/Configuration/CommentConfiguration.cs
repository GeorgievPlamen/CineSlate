using Infrastructure.Database.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<CommentModel>
{
    public void Configure(EntityTypeBuilder<CommentModel> builder)
    {
        builder.HasIndex(x => x.UserId);

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
    }
}