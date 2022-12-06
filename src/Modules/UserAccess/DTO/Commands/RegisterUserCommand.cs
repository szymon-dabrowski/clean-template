using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.DTO.Model;

namespace Clean.Modules.UserAccess.DTO.Commands;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<ErrorOr<AuthResult>>;