using Domain.Common.Models;
using MediatR;

namespace Application.Common;

public static class EventHandler
{
    public static async Task PublishEvents(this IPublisher publisher, IEntity entity, CancellationToken cancellationToken)
    {
        var events = entity.DomainEvents;

        entity.ClearEvents();

        await publisher.Publish(events, cancellationToken);
    }
    public static async Task PublishEvents(this IPublisher publisher, IEnumerable<IEntity> entities, CancellationToken cancellationToken)
    {
        var events = entities.SelectMany(e => e.DomainEvents);

        foreach (var entity in entities)
        {
            entity.ClearEvents();
        }

        await publisher.Publish(events, cancellationToken);
    }
}