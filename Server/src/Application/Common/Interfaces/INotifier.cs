namespace Application.Common.Interfaces;

public interface INotifier
{
    public Task NotifyUser(Guid userId, string topic, object args);
}