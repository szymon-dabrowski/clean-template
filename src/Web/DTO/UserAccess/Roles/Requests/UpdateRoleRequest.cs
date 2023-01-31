namespace Clean.Web.Dto.UserAccess.Roles.Requests;
public record UpdateRoleRequest(string Name, List<string> Permissions);