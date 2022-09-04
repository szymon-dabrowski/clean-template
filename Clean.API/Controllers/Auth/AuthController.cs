using Clean.Application.Auth.Commands;
using Clean.Application.Auth.Model;
using Clean.Application.Auth.Queries;
using Clean.Contracts.Auth.Requests;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clean.API.Controllers.Auth;

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
            authResult => Ok(mapper.Map<AuthResult>(authResult)),
            error => Problem(StatusCodes.Status409Conflict, error));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var authResult = await mediator.Send(mapper.Map<GetTokenQuery>(request));

        return authResult.Match(
            authResult => Ok(mapper.Map<AuthResult>(authResult)),
            error => Problem(StatusCodes.Status401Unauthorized, error));
    }
}
