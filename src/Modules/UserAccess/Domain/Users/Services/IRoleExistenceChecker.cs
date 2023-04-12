using Clean.Modules.UserAccess.Domain.Roles;

namespace Clean.Modules.UserAccess.Domain.Users.Services;
public interface IRoleExistenceChecker
{
    Task<bool> Exists(RoleId roleId);
}