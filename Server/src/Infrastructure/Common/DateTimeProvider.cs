using Application.Common.Interfaces;

namespace Infrastructure.Common;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}
