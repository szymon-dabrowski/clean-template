using Clean.Modules.UserAccess.Dto.Commands;
using Clean.Modules.UserAccess.Dto.Model;
using Clean.Modules.UserAccess.Dto.Queries;
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