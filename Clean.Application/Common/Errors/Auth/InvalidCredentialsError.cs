using Clean.Common.Errors;

namespace Clean.Application.Common.Errors.Auth;
public class InvalidCredentialsError : IError
{
    public string Code => "Auth.InvalidCredentials";
    public string Description => "Invalid credentials!";
}
