namespace Clean.Modules.UserAccess.Application.Dto;

public record AuthResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);