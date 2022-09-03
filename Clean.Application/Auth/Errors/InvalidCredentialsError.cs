using Clean.Common.Errors;

namespace Clean.Application.Auth.Errors;
public class InvalidCredentialsError : IError
{
    public string Code => "Auth.InvalidCredentials";
    public string Description => "Invalid credentials!";
}
