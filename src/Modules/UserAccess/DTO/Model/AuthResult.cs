namespace Clean.Modules.UserAccess.DTO.Model;

public record AuthResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);