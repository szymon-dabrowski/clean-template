using Clean.Application.Common.Interfaces.DateTimeProvider;

namespace Clean.Infrastructure.DateTimeProvider;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
