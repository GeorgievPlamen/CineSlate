using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class ReviewsConfiguration : IEntityTypeConfiguration<ReviewModel>
{
    public void Configure(EntityTypeBuilder<ReviewModel> builder)
    {
        builder.Property(r => r.Rating)
            .IsRequired();

        builder.Property(r => r.AuthorId)
            .IsRequired();

        builder.Property(r => r.Text)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(r => r.ContainsSpoilers)
            .IsRequired();

        builder.HasIndex(r => r.AuthorId);

        builder.Property(r => r.CreatedBy)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.UpdatedBy)
            .IsRequired()
            .HasMaxLength(200);
    }
}