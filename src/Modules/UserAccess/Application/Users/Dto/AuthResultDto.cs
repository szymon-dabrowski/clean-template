namespace Clean.Modules.UserAccess.Application.Users.Dto;
public record AuthResultDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);