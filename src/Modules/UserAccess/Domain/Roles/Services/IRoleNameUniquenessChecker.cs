namespace Clean.Modules.UserAccess.Domain.Roles.Services;
public interface IRoleNameUniquenessChecker
{
    Task<bool> IsUnique(string name);
}