using Clean.Common.Errors;

namespace Clean.Application.Common.Errors.Auth;
public class DuplicatedEmailError : IError
{
    public string Code => "Auth.DuplicatedEmail";
    public string Description => "User with given email already exists.";
}
