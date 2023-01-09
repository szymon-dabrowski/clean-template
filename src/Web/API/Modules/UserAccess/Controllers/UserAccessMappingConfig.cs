using Clean.Modules.UserAccess.Application.Commands.RegisterUser;
using Clean.Modules.UserAccess.Application.Dto;
using Clean.Modules.UserAccess.Application.Queries.GetToken;
using Clean.Web.Dto.UserAccess.Requests;
using Clean.Web.Dto.UserAccess.Responses;
using Mapster;

namespace Clean.Web.Api.Modules.UserAccess.Controllers;

public class UserAccessMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequest, GetTokenQuery>();

        config.NewConfig<RegisterRequest, RegisterUserCommand>();

        config.NewConfig<AuthResult, AuthResponse>();
    }
}