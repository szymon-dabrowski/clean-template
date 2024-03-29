﻿namespace Clean.Modules.Shared.Infrastructure.Permissions;
public interface IPermissionsEnumeration
{
    string GetPermissionName();

    IReadOnlyCollection<IPermissionsEnumeration> GetAllPermissions();
}