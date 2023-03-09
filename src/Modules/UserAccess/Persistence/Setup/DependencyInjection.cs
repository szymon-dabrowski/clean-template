using Clean.Modules.Shared.Persistence.Setup;
using Clean.Modules.Shared.Persistence.UnitOfWork;
using Clean.Modules.UserAccess.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.UserAccess.Persistence.Setup;
public static class DependencyInjection
{
    internal const string UserAccessConnectionStringName = "useraccess";

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<DbContextOptionsBuilder>? dbContextOptionsFactoryBuilder = null)
    {
        services.AddDbContext<DbContext, UserAccessContext>(
            dbContextOptionsFactoryBuilder ?? DefaultDbContextOptionsBuilder(configuration));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddRepositories(typeof(AssemblyMarker).Assembly);

        return services;
    }

    private static Action<DbContextOptionsBuilder> DefaultDbContextOptionsBuilder(
        IConfiguration configuration) => options =>
    {
        options.UseSqlServer(configuration.GetConnectionString(UserAccessConnectionStringName));
    };
}