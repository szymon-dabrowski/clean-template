using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.Shared.Domain;
using Clean.Modules.UserAccess.Domain.Permissions.Rules;
using Clean.Modules.UserAccess.Domain.Permissions.Services;

namespace Clean.Modules.UserAccess.Domain.Permissions;
public class Permission : ValueObject
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
        var result = await Check(new PermissionMustExistRule(name, permissionChecker));

        if (result.IsError)
        {
            return Error.From(result);
        }

        return new Permission(name);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}