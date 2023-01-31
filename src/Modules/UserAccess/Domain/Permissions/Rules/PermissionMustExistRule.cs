using Clean.Modules.Shared.Domain;
using Clean.Modules.UserAccess.Domain.Permissions.Services;

namespace Clean.Modules.UserAccess.Domain.Permissions.Rules;
internal class PermissionMustExistRule : IBussinesRule
{
    private readonly string permission;
    private readonly IPermissionExistenceChecker permissionChecker;

    public PermissionMustExistRule(
        string permission,
        IPermissionExistenceChecker permissionChecker)
    {
        this.permission = permission;
        this.permissionChecker = permissionChecker;
    }

    public string Message => $"Undefined permission: {permission}";

    public Task<bool> IsBroken()
    {
        return Task.FromResult(!permissionChecker.PermissionExists(permission));
    }
}