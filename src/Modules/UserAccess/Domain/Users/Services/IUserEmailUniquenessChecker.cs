namespace Clean.Modules.UserAccess.Domain.Users.Services;
public interface IUserEmailUniquenessChecker
{
    Task<bool> IsUnique(string email);
}