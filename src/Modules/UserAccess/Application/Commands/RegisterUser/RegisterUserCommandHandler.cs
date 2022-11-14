using Clean.Application.Auth.Errors;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Application.Interfaces.Persistance;
using Clean.Modules.UserAccess.Application.Interfaces.Services;
using Clean.Modules.UserAccess.Domain.Entities.User;
using Clean.Modules.UserAccess.DTO.Commands;
using Clean.Modules.UserAccess.DTO.Model;

namespace Clean.Modules.UserAccess.Application.Commands.RegisterUser;

internal class RegisterUserCommandHandler
    : ICommandHandler<RegisterUserCommand, ErrorOr<AuthResult>>
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

    public async Task<ErrorOr<AuthResult>> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
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

        return new AuthResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            token);
    }
}
