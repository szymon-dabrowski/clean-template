using Clean.Modules.Crm.Persistence.Database;
using Clean.Modules.Shared.Persistence.Setup;
using Clean.Modules.Shared.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Crm.Persistence.Setup;
public static class DependencyInjection
{
    private const string CrmConnectionStringName = "crm";

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DbContext, CrmContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(CrmConnectionStringName));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddRepositories(typeof(AssemblyMarker).Assembly);

        return services;
    }
}