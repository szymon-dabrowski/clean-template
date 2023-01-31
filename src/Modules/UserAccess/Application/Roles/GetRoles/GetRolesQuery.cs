using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.UserAccess.Application.Roles.Dto;

namespace Clean.Modules.UserAccess.Application.Roles.GetRoles;
public record GetRolesQuery() : IQuery<List<RoleDto>>;