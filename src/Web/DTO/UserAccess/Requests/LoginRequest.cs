namespace Clean.Web.DTO.UserAccess.Requests;

public record LoginRequest(
    string Email,
    string Password);
