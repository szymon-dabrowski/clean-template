using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.Shared.Domain;
using Clean.Modules.UserAccess.Domain.Permissions;
using Clean.Modules.UserAccess.Domain.Permissions.Services;
using Clean.Modules.UserAccess.Domain.Roles.Rules;
using Clean.Modules.UserAccess.Domain.Roles.Services;

namespace Clean.Modules.UserAccess.Domain.Roles;
public class Role : AggregateRoot<Guid>
{
    private List<Permission> permissions = new();

    private Role()
        : base(Guid.Empty)
    {
    }

    private Role(
        Guid id,
        string name,
        List<Permission> permissions)
        : base(id)
    {
        Name = name;
        this.permissions = permissions;
    }

    public string Name { get; private set; } = string.Empty;

    public IReadOnlyCollection<Permission> Permissions => permissions;

    public static async Task<ErrorOr<Role>> Create(
        string name,
        List<string> permissions,
        IRoleNameUniquenessChecker roleNameUniquenessChecker,
        IPermissionExistenceChecker permissionExistenceChecker)
    {
        var result = await Check(
            new RoleNameMustBeUniqueRule(name, roleNameUniquenessChecker),
            new RoleCannotHaveDuplicatedPermissionsRule(permissions));

        if (result.IsError)
        {
            return Error.From(result);
        }

        var rolePermissions = await RolePermissions(permissions, permissionExistenceChecker)
            .ToListAsync();

        if (rolePermissions.Any(p => p.IsError))
        {
            return rolePermissions
                .Where(p => p.IsError)
                .SelectMany(p => p.Errors)
                .ToArray();
        }

        return new Role(Guid.NewGuid(), name, rolePermissions.Select(p => p.Value).ToList());
    }

    public async Task<ErrorOr<Role>> Update(
        string name,
        List<string> permissions,
        IRoleNameUniquenessChecker roleNameUniquenessChecker,
        IPermissionExistenceChecker permissionExistenceChecker)
    {
        var result = await Check(
            new RoleNameMustBeUniqueRule(name, roleNameUniquenessChecker),
            new RoleCannotHaveDuplicatedPermissionsRule(permissions));

        if (result.IsError)
        {
            return Error.From(result);
        }

        var rolePermissions = await RolePermissions(permissions, permissionExistenceChecker)
            .ToListAsync();

        if (rolePermissions.Any(p => p.IsError))
        {
            return rolePermissions
                .Where(p => p.IsError)
                .SelectMany(p => p.Errors)
                .ToArray();
        }

        this.permissions = rolePermissions.Select(p => p.Value).ToList();
        this.Name = name;

        return this;
    }

    private static async IAsyncEnumerable<ErrorOr<Permission>> RolePermissions(
        List<string> permissions,
        IPermissionExistenceChecker permissionChecker)
    {
        foreach (var permission in permissions)
        {
            var rolePermission = await Permission.Create(permission, permissionChecker);

            if (rolePermission.IsError)
            {
                yield return rolePermission.Errors.ToArray();
            }

            yield return rolePermission;
        }
    }
}