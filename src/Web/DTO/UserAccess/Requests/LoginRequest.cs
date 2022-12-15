namespace Clean.Web.Dto.UserAccess.Requests;

public record LoginRequest(
    string Email,
    string Password);