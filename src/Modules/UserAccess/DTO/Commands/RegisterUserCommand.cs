using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Dto.Model;

namespace Clean.Modules.UserAccess.Dto.Commands;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<ErrorOr<AuthResult>>;