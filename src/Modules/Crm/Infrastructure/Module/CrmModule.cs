using Clean.Modules.Shared.Infrastructure.Module;

namespace Clean.Modules.Crm.Infrastructure.Module;
public class CrmModule : Shared.Infrastructure.Module.Module, ICrmModule
{
    public CrmModule(IModuleServiceProvider moduleServiceProvider)
        : base(moduleServiceProvider)
    {
    }
}