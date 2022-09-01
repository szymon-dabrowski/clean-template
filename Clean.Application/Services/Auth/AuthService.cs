using Clean.Application.Abstractions.Persistance.User;
using Clean.Application.Abstractions.Services.Auth;
using Clean.Application.Common.Errors.Auth;
using Clean.Application.Services.Auth.Model;
using Clean.Domain.Entities.User;
using OneOf;

namespace Clean.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public AuthService(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }

    public OneOf<AuthResult, InvalidCredentialsError> Login(string email, string password)
    {
        var user = userRepository.GetUserByEmail(email);

        if (user == null || user.Password != password)
        {
            return new InvalidCredentialsError();
        }

        var token = jwtTokenGenerator.GenerateJwtToken(user);

        return new AuthResult(user, token);
    }

    public OneOf<AuthResult, DuplicatedEmailError> Register(string firstName, string lastName, string email, string password)
    {
        if (userRepository.GetUserByEmail(email) != null)
        {
            return new DuplicatedEmailError();
        }

        var user = new UserEntity()
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Password = password
        };

        userRepository.AddUser(user);

        var token = jwtTokenGenerator.GenerateJwtToken(user);

        return new AuthResult(user, token);
    }
}
