namespace Clean.Application.Services.Auth.Model;
public record AuthResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);
