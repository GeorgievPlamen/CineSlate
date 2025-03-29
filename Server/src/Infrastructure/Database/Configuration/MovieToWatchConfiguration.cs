using Infrastructure.Database.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class MovieToWatchConfiguration : IEntityTypeConfiguration<MovieToWatchModel>
{
    public void Configure(EntityTypeBuilder<MovieToWatchModel> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
