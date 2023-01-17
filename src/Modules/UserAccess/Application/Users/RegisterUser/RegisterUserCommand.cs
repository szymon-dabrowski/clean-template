using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Application.Users.Dto;

namespace Clean.Modules.UserAccess.Application.Users.RegisterUser;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<ErrorOr<AuthResultDto>>;