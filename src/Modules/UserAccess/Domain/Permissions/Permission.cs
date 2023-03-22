using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Domain.Permissions.Rules;
using Clean.Modules.UserAccess.Domain.Permissions.Services;

namespace Clean.Modules.UserAccess.Domain.Permissions;
public record Permission
{
    private Permission(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public static async Task<ErrorOr<Permission>> Create(
        string name,
        IPermissionExistenceChecker permissionChecker)
    {
        var rule = new PermissionMustExistRule(name, permissionChecker);

        var result = await rule.IsBroken();

        if (result)
        {
            return Error.BusinessRuleBroken(description: rule.Message);
        }

        return new Permission(name);
    }
}