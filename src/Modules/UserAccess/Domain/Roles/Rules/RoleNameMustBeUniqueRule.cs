using Clean.Modules.Shared.Domain;
using Clean.Modules.UserAccess.Domain.Roles.Services;

namespace Clean.Modules.UserAccess.Domain.Roles.Rules;
internal class RoleNameMustBeUniqueRule : IBussinesRule
{
    private readonly string name;
    private readonly IRoleNameUniquenessChecker roleNameUniquenessChecker;

    public RoleNameMustBeUniqueRule(
        string name,
        IRoleNameUniquenessChecker roleNameUniquenessChecker)
    {
        this.name = name;
        this.roleNameUniquenessChecker = roleNameUniquenessChecker;
    }

    public string Message => "Role with given name is already defined.";

    public async Task<bool> IsBroken()
        => !await roleNameUniquenessChecker.IsUnique(this.name);
}