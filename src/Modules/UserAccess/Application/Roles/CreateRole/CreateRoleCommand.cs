using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;

namespace Clean.Modules.UserAccess.Application.Roles.CreateRole;
public record CreateRoleCommand(
    string Name,
    List<string> Permissions) : ICommand<ErrorOr<Guid>>;