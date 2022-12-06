﻿using Clean.Modules.UserAccess.DTO.Commands;
using Clean.Modules.UserAccess.DTO.Queries;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Web.API.Common.Controllers;
using Clean.Web.DTO.UserAccess.Requests;
using Clean.Web.DTO.UserAccess.Responses;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.API.Modules.UserAccess.Controllers;

[AllowAnonymous]
[Route("[controller]")]
public class UsersController : ApiController
{
    private readonly IUserAccessModule userAccessModule;
    private readonly IMapper mapper;

    public UsersController(IUserAccessModule userAccessModule, IMapper mapper)
    {
        this.userAccessModule = userAccessModule;
        this.mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var authResult = await userAccessModule
            .ExecuteCommand(mapper.Map<RegisterUserCommand>(request));

        return authResult.Match(
            authResult => Ok(mapper.Map<AuthResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var authResult = await userAccessModule
            .ExecuteQuery(mapper.Map<GetTokenQuery>(request));

        return authResult.Match(
            authResult => Ok(mapper.Map<AuthResponse>(authResult)),
            errors => Problem(errors, StatusCodes.Status401Unauthorized));
    }
}