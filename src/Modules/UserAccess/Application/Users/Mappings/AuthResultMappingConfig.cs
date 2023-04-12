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
                d => d.Token,
                _ => MapContext.Current!.Parameters[TokenParameter])
            .Map(d => d.Id, s => s.Id.Value);
    }
}