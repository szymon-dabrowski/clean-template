using Clean.Modules.Shared.Infrastructure.Permissions;
using Clean.Modules.UserAccess.Domain.Permissions.Services;

namespace Clean.Modules.UserAccess.Infrastructure.Domain.Permissions.Services;
internal class PermissionExistenceChecker : IPermissionExistenceChecker
{
    private readonly IEnumerable<IPermissionsEnumeration> permissions;

    public PermissionExistenceChecker(IEnumerable<IPermissionsEnumeration> permissions)
    {
        this.permissions = permissions ?? new List<IPermissionsEnumeration>();
    }

    public bool PermissionExists(string permission)
        => permissions
            .Select(p => p.GetPermissionName())
            .Contains(permission);
}