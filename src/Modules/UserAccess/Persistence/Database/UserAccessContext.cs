using Clean.Modules.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.UserAccess.Persistence.Database;
public class UserAccessContext : DbContext
{
    public UserAccessContext(DbContextOptions<UserAccessContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder
            .ApplyConfigurationsFromAssembly(GetType().Assembly);
}