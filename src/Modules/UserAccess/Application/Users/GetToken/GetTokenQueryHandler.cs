using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Application.Users.Dto;
using Clean.Modules.UserAccess.Application.Users.Mappings;
using Clean.Modules.UserAccess.Domain.Users;
using Clean.Modules.UserAccess.Domain.Users.Errors;
using Clean.Modules.UserAccess.Domain.Users.Services;
using Mapster;

namespace Clean.Modules.UserAccess.Application.Users.GetToken;

internal class GetTokenQueryHandler : IQueryHandler<GetTokenQuery, ErrorOr<AuthResultDto>>
{
    private readonly IPasswordHashing passwordHashing;
    private readonly IJwtGenerator jwtGenerator;
    private readonly IUserRepository userRepository;

    public GetTokenQueryHandler(
        IPasswordHashing passwordHashing,
        IJwtGenerator jwtGenerator,
        IUserRepository userRepository)
    {
        this.passwordHashing = passwordHashing;
        this.jwtGenerator = jwtGenerator;
        this.userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthResultDto>> Handle(
        GetTokenQuery query,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmail(query.Email);

        if (user == null)
        {
            return AuthErrors.InvalidCredentials;
        }

        var token = await user.GetToken(
            query.Password,
            passwordHashing,
            jwtGenerator);

        if (token.IsError)
        {
            return token.Errors.ToArray();
        }

        return user.BuildAdapter()
            .AddParameters(AuthResultMappingConfig.TokenParameter, token.Value)
            .AdaptToType<AuthResultDto>();
    }
}