using Clean.Application.Auth.Errors;
using Clean.Application.Auth.Model;
using Clean.Application.Common.Interfaces.Auth;
using Clean.Application.Common.Interfaces.Persistance;
using MediatR;
using OneOf;

namespace Clean.Application.Auth.Queries;

public record GetTokenQuery(
    string Email,
    string Password) : IRequest<OneOf<AuthResult, InvalidCredentialsError>>;


public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, OneOf<AuthResult, InvalidCredentialsError>>
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

    public async Task<OneOf<AuthResult, InvalidCredentialsError>> Handle(GetTokenQuery query, CancellationToken cancellationToken)
    {
        var user = userRepository.GetUserByEmail(query.Email);

        if (user == null || user.Password != query.Password)
        {
            return new InvalidCredentialsError();
        }

        var token = jwtTokenGenerator.GenerateJwtToken(user);

        return new AuthResult(user, token);
    }
}