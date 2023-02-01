using Clean.Modules.Shared.Infrastructure.Permissions;

namespace Clean.Web.Api.Modules.Crm.Customers;

public class CustomersPermissions : PermissionsEnumeration<CustomersPermissions>
{
    public static readonly CustomersPermissions Write = new(1, nameof(Write));
    public static readonly CustomersPermissions Read = new(2, nameof(Read));

    public CustomersPermissions()
        : base(default, nameof(CustomersPermissions))
    {
    }

    protected CustomersPermissions(int id, string name)
        : base(id, name)
    {
    }
}