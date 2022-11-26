using Clean.Modules.Shared.Infrastructure.Module;

namespace Clean.Modules.UserAccess.Infrastructure.Module;

public class UserAccessModule : Shared.Infrastructure.Module.Module, IUserAccessModule
{
    public UserAccessModule(IModuleServiceProvider moduleServiceProvider)
        : base(moduleServiceProvider)
    {
    }
}
