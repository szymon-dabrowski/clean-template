using Clean.Modules.Shared.Domain;
using Clean.Modules.UserAccess.Domain.Users.Services;

namespace Clean.Modules.UserAccess.Domain.Users.Rules;
internal class RoleMustExistRule : IBusinessRule
{
    private readonly Guid roleId;
    private readonly IRoleExistenceChecker roleExistenceChecker;

    public RoleMustExistRule(Guid roleId, IRoleExistenceChecker roleExistenceChecker)
    {
        this.roleId = roleId;
        this.roleExistenceChecker = roleExistenceChecker;
    }

    public string Message => "Role not found.";

    public async Task<bool> IsBroken()
        => !await roleExistenceChecker.Exists(roleId);
}