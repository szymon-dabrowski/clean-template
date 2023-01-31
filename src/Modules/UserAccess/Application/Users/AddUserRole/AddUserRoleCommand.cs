﻿using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.UserAccess.Application.Users.AddUserRole;
public record AddUserRoleCommand(Guid UserId, Guid RoleId) : ICommand<ErrorOr<Unit>>;