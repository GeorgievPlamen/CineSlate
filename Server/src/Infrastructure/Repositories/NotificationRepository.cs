using Application.Common;
using Application.Notifications.Interfaces;

using Domain.Notifications;
using Domain.Notifications.ValueObjects;
using Domain.Users.ValueObjects;

using Infrastructure.Database;
using Infrastructure.Repositories.MappingExtensions;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NotificationRepository(CineSlateContext dbContext) : INotificaitonRepository
{
    public async Task<bool> CreateAsync(NotificationAggregate notification, CancellationToken cancellationToken)
    {
        var model = notification.ToModel();

        dbContext.Add(model);

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<NotificationAggregate?> GetByIdAsync(NotificationId id, CancellationToken cancellationToken)
    {
        var result = await dbContext.Notifications
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken: cancellationToken);

        return result?.Unwrap();
    }

    public async Task<Paged<NotificationAggregate>> GetManyPagedByUserIdAsync(UserId userId, int page, int count, CancellationToken cancellationToken)
    {
        var result = await dbContext.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == userId.Value)
            .OrderBy(r => r.CreatedAt)
            .Skip(count * (page - 1))
            .Take(count)
            .ToListAsync(cancellationToken);

        var total = await dbContext.Reviews.CountAsync(x => x.AuthorId == userId.Value, cancellationToken);

        return new Paged<NotificationAggregate>(
            [.. result.Select(n => n.Unwrap())],
            page,
            total - (page * count) > 0,
            page > 1,
            total);
    }

    public async Task<bool> UpdateAsync(NotificationAggregate notification, CancellationToken cancellationToken)
    {
        var oldNotification = await dbContext.Notifications
            .FirstOrDefaultAsync(r => r.Id == notification.Id.Value, cancellationToken);

        if (oldNotification is null)
            return false;

        oldNotification.Metadata = notification.Metadata;
        oldNotification.Status = notification.Status;

        dbContext.Update(oldNotification);

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}