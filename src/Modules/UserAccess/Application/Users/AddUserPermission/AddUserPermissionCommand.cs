using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.UserAccess.Application.Users.AddUserPermission;
public record AddUserPermissionCommand(Guid UserId, string Permission) : ICommand<ErrorOr<Unit>>;