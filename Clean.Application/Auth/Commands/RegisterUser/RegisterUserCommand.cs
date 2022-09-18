using Clean.Application.Auth.Errors;
using Clean.Application.Auth.Model;
using Clean.Application.Common.Interfaces.Auth;
using Clean.Application.Common.Interfaces.Persistance;
using Clean.Common.Errors;
using Clean.Domain.Entities.User;
using MediatR;

namespace Clean.Application.Auth.Commands.RegisterUser;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthResult>>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<AuthResult>>
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

    public async Task<ErrorOr<AuthResult>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (userRepository.GetUserByEmail(command.Email) != null)
        {
            return AuthErrors.DuplicateEmail;
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
