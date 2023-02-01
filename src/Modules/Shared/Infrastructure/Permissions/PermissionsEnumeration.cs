using Clean.Modules.Shared.Common.Enumerations;

namespace Clean.Modules.Shared.Infrastructure.Permissions;
public class PermissionsEnumeration<TEnum> : Enumeration<TEnum>, IPermissionsEnumeration
    where TEnum : Enumeration<TEnum>, IPermissionsEnumeration
{
    protected PermissionsEnumeration(int value, string name)
        : base(value, name)
    {
    }

    public string GetPermissionName()
    {
        return $"{GetType().Name}.{Name}";
    }

    public IReadOnlyCollection<IPermissionsEnumeration> GetAllPermissions()
    {
        return All;
    }
}