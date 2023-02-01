using Clean.Modules.Shared.Infrastructure.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Clean.Web.Api.Common.Permissions;

public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionAuthorizationRequirement requirement)
    {
        await Task.CompletedTask;

        var permissionsClaims = context.User.Claims
            .Where(c => c.Type == Constants.PermissionsClaim)
            .Select(c => c.Value)
            .ToList();

        if (permissionsClaims.Contains(requirement.Permission))
        {
            context.Succeed(requirement);

            return;
        }

        context.Fail();
    }
}