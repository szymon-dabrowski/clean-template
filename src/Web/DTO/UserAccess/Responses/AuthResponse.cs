namespace Clean.Web.Dto.UserAccess.Responses;

public record AuthResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);