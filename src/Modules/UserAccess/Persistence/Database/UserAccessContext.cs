using Clean.Modules.UserAccess.Domain.Roles;
using Clean.Modules.UserAccess.Domain.Users;
using Clean.Modules.UserAccess.Persistence.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Clean.Modules.UserAccess.Persistence.Database;
public class UserAccessContext : DbContext
{
    private readonly IConfiguration? configuration;

    public UserAccessContext(IConfiguration? configuration)
    {
        this.configuration = configuration;
    }

    public UserAccessContext(DbContextOptions<UserAccessContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Role> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder
            .ApplyConfigurationsFromAssembly(GetType().Assembly);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);

        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        var connectionString = configuration?.GetConnectionString(
            DependencyInjection.UserAccessConnectionStringName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("UserAccess module connection string is empty!");
        }

        optionsBuilder.UseSqlServer(connectionString);
    }
}