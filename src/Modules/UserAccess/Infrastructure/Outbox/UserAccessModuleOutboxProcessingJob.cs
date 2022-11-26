using Clean.Modules.Shared.Infrastructure.Outbox;
using Clean.Modules.UserAccess.Infrastructure.Module;

namespace Clean.Modules.UserAccess.Infrastructure.Outbox;

internal class UserAccessModuleOutboxProcessingJob : OutboxProcessingJob
{
    public UserAccessModuleOutboxProcessingJob() : base(new UserAccessServiceProvider())
    {
    }
}
