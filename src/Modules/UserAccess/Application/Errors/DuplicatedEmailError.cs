using Clean.Modules.Shared.Common.Errors;

namespace Clean.Application.Auth.Errors;

public static partial class AuthErrors
{
    public static Error DuplicateEmail => Error.Custom(
        "Auth.DuplicatedEmail",
        "User with given email already exists.",
        ErrorType.Conflict);
}