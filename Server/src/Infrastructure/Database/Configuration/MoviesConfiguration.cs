using Domain.Movies;
using Domain.Movies.ValueObjects;
using Domain.Users.ValueObjects;
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

        builder.OwnsMany(m => m.Genres, genreBuilder =>
        {
            genreBuilder.WithOwner();
        });

        // TODO redo how we store genres

        builder.OwnsMany(m => m.Ratings, ratingBuilder =>
        {
            ratingBuilder.WithOwner();

            ratingBuilder.Property(r => r.RatedBy)
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value))
                .IsRequired();

            ratingBuilder.HasKey(r => r.RatedBy);
            ratingBuilder.HasIndex(r => r.RatedBy).IsUnique();
        });

        builder.Property(m => m.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.UpdatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.ComplexProperty(m => m.Details);

        builder.Ignore(m => m.DomainEvents);

        builder.Ignore(m => m.Rating);
    }
}
