using Microsoft.AspNetCore.Authorization;

namespace Clean.Web.Api.Common.Permissions;

public class PermissionAuthorizationRequirement : IAuthorizationRequirement
{
    public PermissionAuthorizationRequirement(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}