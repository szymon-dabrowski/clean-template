using Clean.Modules.UserAccess.Domain.Permissions;
using Clean.Modules.UserAccess.Domain.Roles;

namespace Clean.Modules.UserAccess.Domain.Users;
public record UserRole
{
    private readonly Role role = null!;

    public UserRole(RoleId roleId)
    {
        RoleId = roleId;
    }

    public RoleId RoleId { get; }

    public IReadOnlyCollection<Permission> Permissions => role.Permissions;
}