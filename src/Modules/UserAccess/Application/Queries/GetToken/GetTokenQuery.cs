using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Application.Dto;

namespace Clean.Modules.UserAccess.Application.Queries.GetToken;

public record GetTokenQuery(
    string Email,
    string Password) : IQuery<ErrorOr<AuthResult>>;