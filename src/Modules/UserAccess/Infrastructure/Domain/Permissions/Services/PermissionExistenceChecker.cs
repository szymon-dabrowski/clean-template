using Clean.Modules.UserAccess.Domain.Permissions.Services;
using Clean.Modules.UserAccess.Infrastructure.Services;

namespace Clean.Modules.UserAccess.Infrastructure.Domain.Permissions.Services;
internal class PermissionExistenceChecker : IPermissionExistenceChecker
{
    private readonly IEnumerable<IPermissionsModule> permissionsModules;

    public PermissionExistenceChecker(IEnumerable<IPermissionsModule> permissionsModules)
    {
        this.permissionsModules = permissionsModules ?? new List<IPermissionsModule>();
    }

    public bool PermissionExists(string permission)
        => permissionsModules
            .SelectMany(p => p.GetPermissions())
            .Contains(permission);
}