using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.DTO.Model;
using MediatR;

namespace Clean.Modules.UserAccess.DTO.Commands;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthResult>>;

