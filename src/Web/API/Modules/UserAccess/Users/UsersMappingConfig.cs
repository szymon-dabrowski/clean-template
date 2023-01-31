using Clean.Modules.UserAccess.Application.Users.Dto;
using Clean.Modules.UserAccess.Application.Users.GetToken;
using Clean.Modules.UserAccess.Application.Users.RegisterUser;
using Clean.Web.Dto.UserAccess.Users.Requests;
using Clean.Web.Dto.UserAccess.Users.Responses;
using Mapster;

namespace Clean.Web.Api.Modules.UserAccess.Users;

public class UsersMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequest, GetTokenQuery>();

        config.NewConfig<RegisterRequest, RegisterUserCommand>();

        config.NewConfig<AuthResultDto, AuthResponse>();
    }
}