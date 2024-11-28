using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class MoviesConfiguration : IEntityTypeConfiguration<MovieModel>
{
    public void Configure(EntityTypeBuilder<MovieModel> builder)
    {
        builder.HasIndex(m => m.Id)
            .IsUnique();

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

        builder.HasMany(m => m.Genres)
            .WithMany(g => g.Movies);

        builder.Property(m => m.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.UpdatedBy)
            .IsRequired()
            .HasMaxLength(100);

        // TODO rethink domain events

        // builder.Ignore(m => m.DomainEvents);
    }
}
