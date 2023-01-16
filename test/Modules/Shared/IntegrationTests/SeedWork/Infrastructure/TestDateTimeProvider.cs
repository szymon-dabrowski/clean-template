using Clean.Modules.Shared.Application.Interfaces.Services;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Infrastructure;
internal class TestDateTimeProvider : IDateTimeProvider
{
    public TestDateTimeProvider()
    {
        UtcNow = DateTime.UtcNow;
    }

    public DateTime UtcNow { get; set; }
}