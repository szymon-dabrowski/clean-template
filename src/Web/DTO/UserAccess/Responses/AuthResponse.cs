namespace Clean.Web.DTO.UserAccess.Responses;

public record AuthResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);