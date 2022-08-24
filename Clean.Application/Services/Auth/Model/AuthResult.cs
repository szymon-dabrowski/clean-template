using Clean.Domain.Entities.User;

namespace Clean.Application.Services.Auth.Model;
public record AuthResult(
    UserEntity User,
    string Token);
