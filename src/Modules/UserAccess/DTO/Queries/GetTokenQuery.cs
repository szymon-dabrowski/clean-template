using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.DTO.Model;

namespace Clean.Modules.UserAccess.DTO.Queries;

public record GetTokenQuery(
    string Email,
    string Password) : IQuery<ErrorOr<AuthResult>>;
