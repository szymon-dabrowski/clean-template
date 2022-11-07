using Clean.Modules.Shared.Application.Interfaces.Services;

namespace Clean.Modules.Shared.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
