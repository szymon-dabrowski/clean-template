using Clean.Modules.UserAccess.Application.Commands.RegisterUser;
using Clean.Modules.UserAccess.Application.Queries.GetToken;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Web.Api.Common.Controllers;
using Clean.Web.Dto.UserAccess.Requests;
using Clean.Web.Dto.UserAccess.Responses;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.Api.Modules.UserAccess.Controllers;

[AllowAnonymous]
[Route("[controller]")]
public class UsersController : ApiController
{
    private readonly IUserAccessModule userAccessModule;

    public UsersController(IUserAccessModule userAccessModule)
    {
        this.userAccessModule = userAccessModule;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var authResult = await userAccessModule
            .ExecuteCommand(request.Adapt<RegisterUserCommand>());

        return authResult.Match(
            authResult => Ok(authResult.Adapt<AuthResponse>()),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var authResult = await userAccessModule
            .ExecuteQuery(request.Adapt<GetTokenQuery>());

        return authResult.Match(
            authResult => Ok(authResult.Adapt<AuthResponse>()),
            errors => Problem(errors, StatusCodes.Status401Unauthorized));
    }
}