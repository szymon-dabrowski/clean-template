using Clean.Common.Errors;

namespace Clean.Application.Auth.Errors;

public static partial class AuthErrors
{
    public static Error InvalidCredentials => Error.Custom(
        "Auth.InvalidCredentials",
        "Invalid credentials!",
        ErrorType.Failure);
}