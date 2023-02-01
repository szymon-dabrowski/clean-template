namespace Clean.Web.Dto.UserAccess.Users.Requests;

public record LoginRequest(
    string Email,
    string Password);