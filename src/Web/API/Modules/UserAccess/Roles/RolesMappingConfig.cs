using Clean.Modules.UserAccess.Application.Roles.Dto;
using Mapster;

namespace Clean.Web.Api.Modules.UserAccess.Roles;

public class RolesMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RoleDto, Dto.UserAccess.Roles.Model.RoleDto>();
    }
}