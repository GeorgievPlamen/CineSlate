using Infrastructure.Database.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class NotificationConfiguration : IEntityTypeConfiguration<NotificationModel>
{
    public void Configure(EntityTypeBuilder<NotificationModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.UserId);
    }
}