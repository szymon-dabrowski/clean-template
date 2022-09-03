using Clean.Domain.Entities.User;

namespace Clean.Application.Auth.Model;
public record AuthResult(
    UserEntity User,
    string Token);
