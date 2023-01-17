using Clean.Modules.Shared.Common.Errors;

namespace Clean.Modules.UserAccess.Domain.Users.Errors;

public static partial class AuthErrors
{
    public static Error InvalidCredentials => Error.Custom(
        "Auth.InvalidCredentials",
        "Invalid credentials!",
        ErrorType.Failure);
}