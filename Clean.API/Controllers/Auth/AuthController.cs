using Clean.Application.Services.Auth;
using Clean.Application.Services.Auth.Model;
using Clean.Contracts.Auth.Requests;
using Clean.Contracts.Auth.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Clean.API.Controllers.Auth;

[Route("[controller]")]
public class AuthController : ApiController
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = authService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            error => Problem(StatusCodes.Status409Conflict, error));
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = authService.Login(
            request.Email,
            request.Password);

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
