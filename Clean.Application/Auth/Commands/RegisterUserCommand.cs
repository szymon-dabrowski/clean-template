using Clean.Application.Auth.Errors;
using Clean.Application.Auth.Model;
using Clean.Application.Common.Interfaces.Auth;
using Clean.Application.Common.Interfaces.Persistance;
using Clean.Domain.Entities.User;
using MediatR;
using OneOf;

namespace Clean.Application.Auth.Commands;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<OneOf<AuthResult, DuplicatedEmailError>>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OneOf<AuthResult, DuplicatedEmailError>>
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public RegisterUserCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }

    public async Task<OneOf<AuthResult, DuplicatedEmailError>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (userRepository.GetUserByEmail(command.Email) != null)
        {
            return new DuplicatedEmailError();
        }

        var user = new UserEntity()
        {
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Password = command.Password
        };

        userRepository.AddUser(user);

        var token = jwtTokenGenerator.GenerateJwtToken(user);

        return new AuthResult(user, token);
    }
}
