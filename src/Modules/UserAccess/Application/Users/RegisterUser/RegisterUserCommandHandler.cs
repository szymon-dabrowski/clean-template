using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Application.Users.Dto;
using Clean.Modules.UserAccess.Application.Users.Mappings;
using Clean.Modules.UserAccess.Domain.Users;
using Clean.Modules.UserAccess.Domain.Users.Services;
using Mapster;

namespace Clean.Modules.UserAccess.Application.Users.RegisterUser;

internal class RegisterUserCommandHandler
    : ICommandHandler<RegisterUserCommand, ErrorOr<AuthResultDto>>
{
    private readonly IUserEmailUniquenessChecker userEmailUniquenessChecker;
    private readonly IPasswordStrengthChecker passwordStrengthChecker;
    private readonly IPasswordHashing passwordHashing;
    private readonly IJwtGenerator jwtGenerator;
    private readonly IUserRepository usersRepository;

    public RegisterUserCommandHandler(
        IUserEmailUniquenessChecker userEmailUniquenessChecker,
        IPasswordStrengthChecker passwordStrengthChecker,
        IPasswordHashing passwordHashing,
        IJwtGenerator jwtGenerator,
        IUserRepository userRepository)
    {
        this.userEmailUniquenessChecker = userEmailUniquenessChecker;
        this.passwordStrengthChecker = passwordStrengthChecker;
        this.passwordHashing = passwordHashing;
        this.jwtGenerator = jwtGenerator;
        usersRepository = userRepository;
    }

    public async Task<ErrorOr<AuthResultDto>> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password,
            userEmailUniquenessChecker,
            passwordStrengthChecker,
            passwordHashing);

        if (user.IsError)
        {
            return user.Errors.ToArray();
        }

        await usersRepository.AddUser(user.Value);

        var token = await user.Value.GetToken(
            command.Password,
            passwordHashing,
            jwtGenerator);

        return user.Value
            .BuildAdapter()
            .AddParameters(AuthResultMappingConfig.TokenParameter, token.Value)
            .AdaptToType<AuthResultDto>();
    }
}