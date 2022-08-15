namespace Clean.Contracts.Auth.Responses;
public record AuthResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);
