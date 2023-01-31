using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.UserAccess.Application.Roles.UpdateRole;
public record UpdateRoleCommand(
    Guid RoleId,
    string Name,
    List<string> Permissions) : ICommand<ErrorOr<Unit>>;