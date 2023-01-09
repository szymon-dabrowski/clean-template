using Clean.Application.Auth.Errors;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Application.Dto;
using Clean.Modules.UserAccess.Application.Interfaces.Persistence;
using Clean.Modules.UserAccess.Application.Interfaces.Services;

namespace Clean.Modules.UserAccess.Application.Queries.GetToken;

internal class GetTokenQueryHandler : IQueryHandler<GetTokenQuery, ErrorOr<AuthResult>>
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public GetTokenQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthResult>> Handle(
        GetTokenQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var user = userRepository.GetUserByEmail(query.Email);

        if (user == null || user.Password != query.Password)
        {
            return AuthErrors.InvalidCredentials;
        }

        var token = jwtTokenGenerator.GenerateJwtToken(user);

        return new AuthResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            token);
    }
}