namespace Clean.Web.Dto.UserAccess.Roles.Model;
public record RoleDto(Guid Id, string Name, List<string> Permissions);