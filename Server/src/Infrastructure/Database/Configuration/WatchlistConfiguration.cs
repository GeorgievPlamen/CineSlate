using Infrastructure.Database.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class WatchlistConfiguration : IEntityTypeConfiguration<WatchlistModel>
{
    public void Configure(EntityTypeBuilder<WatchlistModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.UserId).IsUnique();

        builder.HasMany(x => x.MovieToWatchModels)
            .WithOne(y => y.Watchlist);
    }
}
