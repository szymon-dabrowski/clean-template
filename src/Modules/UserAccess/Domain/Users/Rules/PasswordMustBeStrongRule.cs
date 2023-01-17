using Clean.Modules.Shared.Domain;
using Clean.Modules.UserAccess.Domain.Users.Services;

namespace Clean.Modules.UserAccess.Domain.Users.Rules;
internal partial class PasswordMustBeStrongRule : IBussinesRule
{
    private readonly string password;
    private readonly IPasswordStrengthChecker passwordStrengthChecker;

    public PasswordMustBeStrongRule(
        string password,
        IPasswordStrengthChecker passwordStrengthChecker)
    {
        this.password = password;
        this.passwordStrengthChecker = passwordStrengthChecker;
    }

    public string Message => "Password has to be at least 8 character " +
        "long and it has to contain at least one capital case and digit.";

    public Task<bool> IsBroken()
    {
        return Task.FromResult(!passwordStrengthChecker.IsStrong(password));
    }
}