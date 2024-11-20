using Domain.Movies;
using Domain.Movies.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class MoviesConfiguration : IEntityTypeConfiguration<MovieAggregate>
{
    public void Configure(EntityTypeBuilder<MovieAggregate> builder)
    {
        builder.Property(m => m.Id)
            .HasConversion(id => id.Value, value => MovieId.Create(value));

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

        builder.Property(u => u.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.UpdatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Ignore(m => m.DomainEvents);

        builder.Ignore(m => m.Rating);
    }
}
