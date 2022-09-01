using Clean.Application.Common.Errors.Auth;
using Clean.Application.Services.Auth.Model;
using OneOf;

namespace Clean.Application.Services.Auth;
public interface IAuthService
{
    OneOf<AuthResult, InvalidCredentialsError> Login(string email, string password);
    OneOf<AuthResult, DuplicatedEmailError> Register(string firstName, string lastName, string email, string password);
}
