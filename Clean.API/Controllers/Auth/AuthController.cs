using Clean.Application.Services.Auth;
using Clean.Contracts.Auth.Requests;
using Clean.Contracts.Auth.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Clean.API.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("register")]
    public AuthResponse Register(RegisterRequest request)
    {
        var authResult = authService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return new AuthResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
    }

    [HttpPost("login")]
    public AuthResponse Login(LoginRequest request)
    {
        var authResult = authService.Login(
            request.Email,
            request.Password);

        return new AuthResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
    }
}
