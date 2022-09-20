using Clean.Application.Auth.Commands.RegisterUser;
using Clean.Application.Auth.Queries.GetToken;
using Clean.Contracts.Auth.Requests;
using Clean.Contracts.Auth.Responses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clean.API.Controllers.Auth;

[AllowAnonymous]
[Route("[controller]")]
public class AuthController : ApiController
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public AuthController(IMediator mediator, IMapper mapper)
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
