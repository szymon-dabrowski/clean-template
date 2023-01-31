using Clean.Modules.Shared.Domain;
using Clean.Modules.UserAccess.Domain.Permissions;
using Clean.Modules.UserAccess.Domain.Roles;

namespace Clean.Modules.UserAccess.Domain.Users;
public class UserRole : ValueObject
{
    private readonly Role role = null!;

    public UserRole(Guid roleId)
    {
        RoleId = roleId;
    }

    public Guid RoleId { get; }

    public IReadOnlyCollection<Permission> Permissions => role.Permissions;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return RoleId;
    }
}