using Clean.Modules.Shared.Infrastructure;

namespace Clean.Modules.UserAccess.Infrastructure.Module;

public class UserAccessModule : Shared.Infrastructure.Module, IUserAccessModule
{
    public UserAccessModule(IModuleServiceProvider moduleServiceProvider)
        : base(moduleServiceProvider)
    {
    }
}