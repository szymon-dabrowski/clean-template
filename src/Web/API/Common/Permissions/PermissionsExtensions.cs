using Clean.Modules.Shared.Infrastructure.Permissions;
using Clean.Web.Api.Modules.Crm.Customers;

namespace Clean.Web.Api.Common.Permissions;

internal static class PermissionsExtensions
{
    public static TBuilder RequirePermission<TBuilder>(
        this TBuilder builder,
        params IPermissionsEnumeration[] permission)
        where TBuilder : IEndpointConventionBuilder
    {
        var permissions = permission
            .Select(p => p.GetPermissionName())
            .ToArray();

        return builder.RequireAuthorization(permissions);
    }

    public static IServiceCollection AddPermissions(this IServiceCollection services)
    {
        services.Scan(s => s.FromAssembliesOf(typeof(PermissionsExtensions))
            .AddClasses(c => c.AssignableTo<IPermissionsEnumeration>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        return services;
    }
}