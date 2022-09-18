using Clean.Application.Auth.Errors;
using Clean.Application.Auth.Model;
using Clean.Application.Common.Interfaces.Auth;
using Clean.Application.Common.Interfaces.Persistance;
using Clean.Common.Errors;
using MediatR;

namespace Clean.Application.Auth.Queries.GetToken;

public record GetTokenQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthResult>>;


public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, ErrorOr<AuthResult>>
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

    public async Task<ErrorOr<AuthResult>> Handle(GetTokenQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var user = userRepository.GetUserByEmail(query.Email);

        if (user == null || user.Password != query.Password)
        {
            return AuthErrors.InvalidCredentials;
        }

        var token = jwtTokenGenerator.GenerateJwtToken(user);

        return new AuthResult(user, token);
    }
}