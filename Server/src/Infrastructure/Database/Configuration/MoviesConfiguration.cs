using Infrastructure.Database.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class MoviesConfiguration : IEntityTypeConfiguration<MovieModel>
{
    public void Configure(EntityTypeBuilder<MovieModel> builder)
    {
        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(m => m.ReleaseDate)
            .IsRequired();

        builder.Property(m => m.PosterPath)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.BackdropPath)
            .HasMaxLength(200);

        builder.Property(m => m.Homepage)
            .HasMaxLength(200);

        builder.Property(m => m.ImdbId)
            .HasMaxLength(50);

        builder.Property(m => m.OriginCountry)
            .HasMaxLength(10);

        builder.Property(m => m.Status)
            .HasMaxLength(20);

        builder.Property(m => m.Tagline)
            .HasMaxLength(200);

        builder.HasMany(m => m.Genres)
            .WithMany(g => g.Movies);

        builder.Property(m => m.CreatedBy)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.UpdatedBy)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(m => m.Reviews)
            .WithOne(r => r.Movie);

        builder.Property(m => m.Rating)
            .HasPrecision(2);

        builder.Property(x => x.Version)
            .IsRowVersion();
    }
}
