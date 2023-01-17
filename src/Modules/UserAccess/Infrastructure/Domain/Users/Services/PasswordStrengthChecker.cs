using System.Text.RegularExpressions;

namespace Clean.Modules.UserAccess.Domain.Users.Services;
internal partial class PasswordStrengthChecker : IPasswordStrengthChecker
{
    public bool IsStrong(string password)
    {
        return PasswordStrengthRegex().IsMatch(password);
    }

    [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$")]
    private partial Regex PasswordStrengthRegex();
}