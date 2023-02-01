using Clean.Modules.Shared.Infrastructure.Permissions;

namespace Clean.Web.Api.Modules.Crm.Orders;

public class OrdersPermissions : PermissionsEnumeration<OrdersPermissions>
{
    public static readonly OrdersPermissions Write = new(1, nameof(Write));
    public static readonly OrdersPermissions Read = new(2, nameof(Read));

    public OrdersPermissions()
        : base(default, nameof(OrdersPermissions))
    {
    }

    protected OrdersPermissions(int value, string name)
        : base(value, name)
    {
    }
}