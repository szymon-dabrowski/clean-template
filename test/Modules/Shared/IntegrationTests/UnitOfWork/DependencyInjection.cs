using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.IntegrationTests.SeedWork;
using Clean.Modules.Shared.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Shared.IntegrationTests.UnitOfWork;
internal static class DependencyInjection
{
    public static IServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IDateTimeProvider, TestDateTimeProvider>();
        services.AddDbContext<DbContext, TestDbContext>(options =>
        {
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
        });
        services.AddTransient<IUnitOfWork, Persistence.UnitOfWork>();

        return services.BuildServiceProvider();
    }
}