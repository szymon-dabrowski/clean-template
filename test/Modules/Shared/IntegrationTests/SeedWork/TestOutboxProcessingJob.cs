using Clean.Modules.Shared.Infrastructure;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork;
internal class TestOutboxProcessingJob : Infrastructure.Outbox.OutboxProcessingJob
{
    public TestOutboxProcessingJob(IModuleServiceProvider moduleServiceProvider)
        : base(moduleServiceProvider)
    {
    }
}
