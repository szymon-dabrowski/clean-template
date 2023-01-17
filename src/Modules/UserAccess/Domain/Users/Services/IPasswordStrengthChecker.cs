namespace Clean.Modules.UserAccess.Domain.Users.Services;
public interface IPasswordStrengthChecker
{
    bool IsStrong(string password);
}