using Clean.Modules.Shared.Infrastructure.Permissions;

namespace Clean.Web.Api.Modules.Crm.Items;

public class ItemsPermissions : PermissionsEnumeration<ItemsPermissions>
{
    public static readonly ItemsPermissions Write = new(1, nameof(Write));
    public static readonly ItemsPermissions Read = new(2, nameof(Read));

    public ItemsPermissions()
        : base(default, nameof(ItemsPermissions))
    {
    }

    protected ItemsPermissions(int value, string name)
        : base(value, name)
    {
    }
}