using Clean.Modules.Shared.Infrastructure.Permissions;

namespace Clean.Web.Api.Modules.UserAccess.Roles;

public class RolesPermissions : PermissionsEnumeration<RolesPermissions>
{
    public static readonly RolesPermissions Write = new(1, nameof(Write));
    public static readonly RolesPermissions Read = new(2, nameof(Read));

    public RolesPermissions()
        : base(default, nameof(RolesPermissions))
    {
    }

    protected RolesPermissions(int value, string name)
        : base(value, name)
    {
    }
}