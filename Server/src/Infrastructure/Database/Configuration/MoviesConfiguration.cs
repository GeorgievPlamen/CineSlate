using Domain.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class MoviesConfiguration : IEntityTypeConfiguration<MovieAggregate>
{
    public void Configure(EntityTypeBuilder<MovieAggregate> builder)
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

        builder.OwnsMany(m => m.Genres)
            .WithOwner();

        builder.OwnsMany(m => m.Ratings)
            .WithOwner();

        builder.Property(m => m.Rating)
            .IsRequired();
    }
}
