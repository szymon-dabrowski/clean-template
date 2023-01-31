using Clean.Modules.Shared.Domain;

namespace Clean.Modules.UserAccess.Domain.Roles.Rules;
internal class RoleCannotHaveDuplicatedPermissionsRule : IBussinesRule
{
    private readonly List<string> permissions;

    public RoleCannotHaveDuplicatedPermissionsRule(List<string> permissions)
    {
        this.permissions = permissions;
    }

    public string Message => "Role cannot have duplicated permissions.";

    public Task<bool> IsBroken()
        => Task.FromResult(permissions.GroupBy(p => p).Any(g => g.Count() > 1));
}