namespace Clean.Modules.UserAccess.Domain.Roles;
public interface IRoleRepository
{
    Task<Role?> GetById(Guid roleId);

    Task Add(Role role);

    void Delete(Guid roleId);
}