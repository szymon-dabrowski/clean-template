using Clean.Modules.UserAccess.DTO.Commands;
using Clean.Modules.UserAccess.DTO.Queries;
using Clean.Web.API.Common.Controllers;
using Clean.Web.DTO.UserAccess.Requests;
using Clean.Web.DTO.UserAccess.Responses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.API.Modules.UserAccess.Controllers;

[AllowAnonymous]
[Route("[controller]")]
public class UsersController : ApiController
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public UsersController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var authResult = await mediator.Send(mapper.Map<RegisterUserCommand>(request));

        return authResult.Match(
            authResult => Ok(mapper.Map<AuthResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var authResult = await mediator.Send(mapper.Map<GetTokenQuery>(request));

        return authResult.Match(
            authResult => Ok(mapper.Map<AuthResponse>(authResult)),
            errors => Problem(errors, StatusCodes.Status401Unauthorized));
    }
}
