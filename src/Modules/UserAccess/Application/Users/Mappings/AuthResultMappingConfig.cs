using Clean.Modules.UserAccess.Application.Users.Dto;
using Clean.Modules.UserAccess.Domain.Users;
using Mapster;

namespace Clean.Modules.UserAccess.Application.Users.Mappings;
internal class AuthResultMappingConfig : IRegister
{
    public const string TokenParameter = "Token";

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, AuthResultDto>()
            .Map(
                dest => dest.Token,
                _ => MapContext.Current!.Parameters[TokenParameter]);
    }
}