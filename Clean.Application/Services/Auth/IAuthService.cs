using Clean.Application.Services.Auth.Model;

namespace Clean.Application.Services.Auth;
public interface IAuthService
{
    AuthResult Login(string email, string password);
    AuthResult Register(string firstName, string lastName, string email, string password);
}
