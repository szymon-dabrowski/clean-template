using Clean.Application.Common.Interfaces.Services;

namespace Clean.Infrastructure.Services.DateTimeProvider;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
