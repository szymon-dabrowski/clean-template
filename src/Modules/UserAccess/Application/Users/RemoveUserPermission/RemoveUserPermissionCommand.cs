using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.UserAccess.Application.Users.RemoveUserPermission;
public record RemoveUserPermissionCommand(Guid UserId, string Permission) : ICommand<ErrorOr<Unit>>;