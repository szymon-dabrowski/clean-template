using Clean.Modules.UserAccess.Application.Users.GetToken;
using Clean.Modules.UserAccess.Application.Users.RegisterUser;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Web.Api.Common.Endpoints;
using Clean.Web.Api.Common.Errors;
using Clean.Web.Dto.UserAccess.Requests;
using Clean.Web.Dto.UserAccess.Responses;
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
            var authResult = await userAccessModule
                .ExecuteCommand(request.Adapt<RegisterUserCommand>());

            return authResult.Match(
                authResult => Results.Ok(authResult.Adapt<AuthResponse>()),
                errors => errors.AsProblem());
        })
            .AllowAnonymous();

        app.MapPost($"{UsersRoute}/login", async (
            IUserAccessModule userAccessModule,
            LoginRequest request) =>
        {
            var authResult = await userAccessModule
                .ExecuteQuery(request.Adapt<GetTokenQuery>());

            return authResult.Match(
                authResult => Results.Ok(authResult.Adapt<AuthResponse>()),
                errors => errors.AsProblem(StatusCodes.Status401Unauthorized));
        })
            .AllowAnonymous();
    }
}