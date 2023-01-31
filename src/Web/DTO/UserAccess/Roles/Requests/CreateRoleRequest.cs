namespace Clean.Web.Dto.UserAccess.Roles.Requests;
public record CreateRoleRequest(string Name, List<string> Permissions);