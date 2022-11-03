using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.DTO.Model;
using MediatR;

namespace Clean.Modules.UserAccess.DTO.Queries;

public record GetTokenQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthResult>>;
