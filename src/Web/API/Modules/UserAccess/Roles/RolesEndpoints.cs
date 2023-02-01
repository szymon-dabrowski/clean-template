using Clean.Modules.UserAccess.Application.Roles.CreateRole;
using Clean.Modules.UserAccess.Application.Roles.DeleteRole;
using Clean.Modules.UserAccess.Application.Roles.GetRoles;
using Clean.Modules.UserAccess.Application.Roles.UpdateRole;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Web.Api.Common.Endpoints;
using Clean.Web.Api.Common.Errors;
using Clean.Web.Api.Common.Permissions;
using Clean.Web.Dto.UserAccess.Roles.Model;
using Clean.Web.Dto.UserAccess.Roles.Requests;
using Clean.Web.Dto.UserAccess.Roles.Responses;
using Mapster;

namespace Clean.Web.Api.Modules.UserAccess.Roles;

internal class RolesEndpoints : IEndpointsModule
{
    private const string RolesRoute = "/roles";

    public void RegisterEndpoints(WebApplication app)
    {
        app.MapGet(RolesRoute, async (IUserAccessModule userAccessModule) =>
        {
            return (await userAccessModule.ExecuteQuery(new GetRolesQuery()))
                .Select(r => r.Adapt<RoleDto>());
        })
            .RequirePermission(RolesPermissions.Read);

        app.MapPost(RolesRoute, async (
            IUserAccessModule userAccessModule,
            CreateRoleRequest request) =>
        {
            var result = await userAccessModule
                .ExecuteCommand(new CreateRoleCommand(request.Name, request.Permissions));

            return result.Match(
                result => Results.Ok(new CreateRoleResponse(result)),
                errors => errors.AsProblem());
        })
            .RequirePermission(RolesPermissions.Write);

        app.MapPut($"{RolesRoute}/{{roleId}}", async (
            IUserAccessModule userAccessModule,
            Guid roleId,
            UpdateRoleRequest request) =>
        {
            var result = await userAccessModule
                .ExecuteCommand(new UpdateRoleCommand(roleId, request.Name, request.Permissions));

            return result.Match(
                _ => Results.Ok(),
                errors => errors.AsProblem());
        })
            .RequirePermission(RolesPermissions.Write);

        app.MapDelete($"{RolesRoute}/{{roleId}}", async (
            IUserAccessModule userAcccessModule,
            Guid roleId) =>
        {
            await userAcccessModule.ExecuteCommand(new DeleteRoleCommand(roleId));
        })
            .RequirePermission(RolesPermissions.Write);
    }
}