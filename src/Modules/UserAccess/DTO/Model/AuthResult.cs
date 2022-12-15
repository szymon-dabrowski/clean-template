namespace Clean.Modules.UserAccess.Dto.Model;

public record AuthResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);