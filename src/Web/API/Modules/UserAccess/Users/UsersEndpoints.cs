using Clean.Modules.UserAccess.Application.Users.AddUserPermission;
using Clean.Modules.UserAccess.Application.Users.AddUserRole;
using Clean.Modules.UserAccess.Application.Users.DeleteUser;
using Clean.Modules.UserAccess.Application.Users.GetToken;
using Clean.Modules.UserAccess.Application.Users.RegisterUser;
using Clean.Modules.UserAccess.Application.Users.RemoveUserPermission;
using Clean.Modules.UserAccess.Application.Users.RemoveUserRole;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Web.Api.Common.Endpoints;
using Clean.Web.Api.Common.Errors;
using Clean.Web.Api.Common.Permissions;
using Clean.Web.Dto.UserAccess.Users.Requests;
using Clean.Web.Dto.UserAccess.Users.Responses;
using Mapster;

namespace Clean.Web.Api.Modules.UserAccess.Users;

internal class UsersEndpoints : IEndpointsModule
{
    private const string UsersRoute = "/users";

    public void RegisterEndpoints(WebApplication app)
    {
        app.MapPost($"{UsersRoute}/register", async (
            IUserAccessModule userAccessModule,
            RegisterRequest request) =>
        {
            var result = await userAccessModule
                .ExecuteCommand(request.Adapt<RegisterUserCommand>());

            return result.Match(
                result => Results.Ok(result.Adapt<AuthResponse>()),
                errors => errors.AsProblem());
        })
            .AllowAnonymous();

        app.MapPost($"{UsersRoute}/login", async (
            IUserAccessModule userAccessModule,
            LoginRequest request) =>
        {
            var result = await userAccessModule
                .ExecuteQuery(request.Adapt<GetTokenQuery>());

            return result.Match(
                result => Results.Ok(result.Adapt<AuthResponse>()),
                errors => errors.AsProblem(StatusCodes.Status401Unauthorized));
        })
            .AllowAnonymous();

        app.MapDelete($"{UsersRoute}/{{userId}}", async (
            IUserAccessModule userAccessModule,
            Guid userId) =>
        {
            await userAccessModule.ExecuteCommand(new DeleteUserCommand(userId));
        })
            .RequirePermission(UsersPermissions.Admin);

        app.MapPost($"{UsersRoute}/{{userId}}/permissions", async (
            IUserAccessModule userAccessModule,
            Guid userId,
            AddUserPermissionRequest request) =>
        {
            var result = await userAccessModule
                .ExecuteCommand(new AddUserPermissionCommand(userId, request.Permission));

            return result.Match(
                _ => Results.Ok(),
                errors => errors.AsProblem());
        })
            .RequirePermission(UsersPermissions.Admin);

        app.MapDelete($"{UsersRoute}/{{userId}}/permissions/{{permission}}", async (
            IUserAccessModule userAccessModule,
            Guid userId,
            string permission) =>
        {
            await userAccessModule
                .ExecuteCommand(new RemoveUserPermissionCommand(userId, permission));
        })
            .RequirePermission(UsersPermissions.Admin);

        app.MapPost($"{UsersRoute}/{{userId}}/roles", async (
            IUserAccessModule userAccessModule,
            Guid userId,
            AddUserRoleRequest request) =>
        {
            var result = await userAccessModule
                .ExecuteCommand(new AddUserRoleCommand(userId, request.RoleId));

            return result.Match(
                _ => Results.Ok(),
                errors => errors.AsProblem());
        })
            .RequirePermission(UsersPermissions.Admin);

        app.MapDelete($"{UsersRoute}/{{userId}}/roles/{{roleId}}", async (
            IUserAccessModule userAccessModule,
            Guid userId,
            Guid roleId) =>
        {
            await userAccessModule.ExecuteCommand(new RemoveUserRoleCommand(userId, roleId));
        })
            .RequirePermission(UsersPermissions.Admin);
    }
}