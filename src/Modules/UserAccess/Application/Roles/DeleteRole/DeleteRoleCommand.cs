using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.UserAccess.Application.Roles.DeleteRole;
public record DeleteRoleCommand(Guid RoleId) : ICommand;