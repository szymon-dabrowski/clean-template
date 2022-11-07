using Clean.Modules.UserAccess.DTO.Commands;
using Clean.Modules.UserAccess.DTO.Model;
using Clean.Modules.UserAccess.DTO.Queries;
using Clean.Web.DTO.UserAccess.Requests;
using Clean.Web.DTO.UserAccess.Responses;
using Mapster;

namespace Clean.Web.API.Modules.UserAccess.Controllers;

public class UserAccessMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequest, GetTokenQuery>();

        config.NewConfig<RegisterRequest, RegisterUserCommand>();

        config.NewConfig<AuthResult, AuthResponse>();
    }
}
