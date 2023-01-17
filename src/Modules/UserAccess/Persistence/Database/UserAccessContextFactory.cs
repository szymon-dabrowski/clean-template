using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Clean.Modules.UserAccess.Persistence.Database;
internal class UserAccessContextFactory : IDesignTimeDbContextFactory<UserAccessContext>
{
    public UserAccessContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UserAccessContext>();

        optionsBuilder.UseSqlServer("Server=localhost;Database=clean;User ID=tba;Password=tba;TrustServerCertificate=True");

        return new UserAccessContext(optionsBuilder.Options);
    }
}