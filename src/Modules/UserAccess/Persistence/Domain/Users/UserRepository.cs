﻿using Clean.Modules.UserAccess.Domain.Users;
using Clean.Modules.UserAccess.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.UserAccess.Persistence.Domain.Users;

internal class UserRepository : IUserRepository
{
    private readonly UserAccessContext userAccessContext;

    public UserRepository(UserAccessContext userAccessContext)
    {
        this.userAccessContext = userAccessContext;
    }

    public async Task AddUser(User user)
    {
        await userAccessContext.AddAsync(user);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await userAccessContext.Users
            .SingleOrDefaultAsync(u => u.Email == email);
    }
}