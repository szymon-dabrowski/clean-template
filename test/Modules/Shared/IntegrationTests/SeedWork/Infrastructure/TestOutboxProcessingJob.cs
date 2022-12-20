using Clean.Modules.Shared.Infrastructure.Module;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Infrastructure;
internal class TestOutboxProcessingJob : Shared.Infrastructure.Outbox.OutboxProcessingJob
{
    public TestOutboxProcessingJob(IModuleServiceProvider moduleServiceProvider)
        : base(moduleServiceProvider)
    {
    }
}