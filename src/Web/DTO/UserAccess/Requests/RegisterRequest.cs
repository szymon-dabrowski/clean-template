namespace Clean.Web.DTO.UserAccess.Requests;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);