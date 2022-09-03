using Clean.Application.Auth.Commands;
using Clean.Application.Auth.Model;
using Clean.Application.Auth.Queries;
using Clean.Contracts.Auth.Requests;
using Clean.Contracts.Auth.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clean.API.Controllers.Auth;

[Route("[controller]")]
public class AuthController : ApiController
{
    private readonly IMediator mediator;

    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var authResult = await mediator.Send(new RegisterUserCommand(request.FirstName,
            request.LastName,
            request.Email,
            request.Password));

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            error => Problem(StatusCodes.Status409Conflict, error));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var authResult = await mediator.Send(new GetTokenQuery(
            request.Email,
            request.Password));

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            error => Problem(StatusCodes.Status401Unauthorized, error));
    }

    private static AuthResponse MapAuthResult(AuthResult authResult)
    {
        return new AuthResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
    }
}
