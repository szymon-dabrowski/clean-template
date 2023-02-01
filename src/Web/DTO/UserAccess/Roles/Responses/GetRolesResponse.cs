using Clean.Web.Dto.UserAccess.Roles.Model;

namespace Clean.Web.Dto.UserAccess.Roles.Responses;
public record GetRolesResponse(List<RoleDto> Roles);