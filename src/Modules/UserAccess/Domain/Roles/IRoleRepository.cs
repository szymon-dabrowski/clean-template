namespace Clean.Modules.UserAccess.Domain.Roles;
public interface IRoleRepository
{
    Task<Role?> GetById(RoleId roleId);

    Task Add(Role role);

    void Delete(RoleId roleId);
}