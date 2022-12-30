using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Clean.Modules.Crm.Persistence.Database;
internal class CrmContextFactory : IDesignTimeDbContextFactory<CrmContext>
{
    public CrmContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CrmContext>();

        optionsBuilder.UseSqlServer("Server=localhost;Database=clean;User ID=tba;Password=tba;TrustServerCertificate=True");

        return new CrmContext(optionsBuilder.Options);
    }
}