namespace Clean.Modules.UserAccess.Domain.Users.Services;
public interface IRoleExistenceChecker
{
    Task<bool> Exists(Guid roleId);
}