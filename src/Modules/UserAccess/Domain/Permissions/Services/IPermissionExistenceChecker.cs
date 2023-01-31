namespace Clean.Modules.UserAccess.Domain.Permissions.Services;
public interface IPermissionExistenceChecker
{
    bool PermissionExists(string permission);
}