using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Modules.Shared.Infrastructure.Outbox;

namespace Clean.Modules.Crm.Infrastructure.Outbox;

internal class CrmModuleOutboxProcessingJob : OutboxProcessingJob
{
    public CrmModuleOutboxProcessingJob()
        : base(new CrmServiceProvider())
    {
    }
}