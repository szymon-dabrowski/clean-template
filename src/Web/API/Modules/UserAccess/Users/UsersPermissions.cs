using Clean.Modules.Shared.Infrastructure.Permissions;

namespace Clean.Web.Api.Modules.UserAccess.Users;

public class UsersPermissions : PermissionsEnumeration<UsersPermissions>
{
    public static readonly UsersPermissions Admin = new(1, nameof(Admin));

    public UsersPermissions()
        : base(default, nameof(UsersPermissions))
    {
    }

    protected UsersPermissions(int value, string name)
        : base(value, name)
    {
    }
}