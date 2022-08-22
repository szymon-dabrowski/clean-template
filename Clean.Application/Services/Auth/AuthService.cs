using Clean.Application.Common.Interfaces.Auth;
using Clean.Application.Services.Auth.Model;

namespace Clean.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;

    public AuthService(IJwtTokenGenerator jwtTokenGenerator)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthResult Login(string email, string password)
    {
        return new AuthResult(
            Guid.NewGuid(),
            "firstName",
            "lastName",
            email,
            "token");
    }

    public AuthResult Register(string firstName, string lastName, string email, string password)
    {
        var userId = Guid.NewGuid();

        var token = jwtTokenGenerator.GenerateJwtToken(userId, firstName, lastName);

        return new AuthResult(
            userId,
            firstName,
            lastName,
            email,
            token);
    }
}
