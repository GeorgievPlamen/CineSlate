namespace Application.Common.Interfaces;

public interface IDateTimeProvider
{
    DateTimeOffset Now { get; }
}