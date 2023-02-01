namespace Clean.Modules.UserAccess.Application.Roles.Dto;
public record RoleDto(Guid Id, string Name, List<string> Permissions);