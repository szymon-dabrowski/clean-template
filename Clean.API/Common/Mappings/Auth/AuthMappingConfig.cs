using Clean.Application.Auth.Commands.RegisterUser;
using Clean.Application.Auth.Model;
using Clean.Application.Auth.Queries.GetToken;
using Clean.Contracts.Auth.Requests;
using Clean.Contracts.Auth.Responses;
using Mapster;

namespace Clean.API.Common.Mappings.Auth;

public class AuthMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequest, GetTokenQuery>();

        config.NewConfig<RegisterRequest, RegisterUserCommand>();

        config.NewConfig<AuthResult, AuthResponse>()
            .Map(dest => dest, src => src.User);
    }
}
