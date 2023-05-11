using Clean.Modules.UserAccess.Application.Roles.Dto;
using Clean.Modules.UserAccess.Domain.Roles;
using Mapster;

namespace Clean.Modules.UserAccess.Application.Roles.Mappings;
internal class RoleMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Role, RoleDto>()
            .MapWith(r => new RoleDto(
                r.Id.Value,
                r.Name,
                r.Permissions.Select(p => p.Name).ToList()));
    }
}