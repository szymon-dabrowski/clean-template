using Clean.Modules.Shared.Domain;
using Clean.Modules.UserAccess.Domain.Permissions;

namespace Clean.Modules.UserAccess.Domain.Users.Rules;
internal class UserCannotHaveDuplicatedPermissionsRule : IBussinesRule
{
    private readonly string permission;
    private readonly List<Permission> existingPermissions;

    public UserCannotHaveDuplicatedPermissionsRule(
        string permission,
        List<Permission> existingPermissions)
    {
        this.permission = permission;
        this.existingPermissions = existingPermissions ?? new();
    }

    public string Message => "User cannot have duplicated permissions.";

    public Task<bool> IsBroken()
        => Task.FromResult(existingPermissions.Any(p => p.Name == permission));
}