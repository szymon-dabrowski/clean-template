using Clean.Modules.Shared.Application.Interfaces.Services;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork;
internal class TestDateTimeProvider : IDateTimeProvider
{
    private DateTime now = DateTime.UtcNow;
    public void SetNow(DateTime now) => this.now = now;
    public DateTime UtcNow => now;
}
