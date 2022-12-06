using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.IntegrationTests.SeedWork;
using Clean.Modules.Shared.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UoW = Clean.Modules.Shared.Persistence.UnitOfWork.UnitOfWork;

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
        services.AddTransient<IUnitOfWork, UoW>();

        return services.BuildServiceProvider();
    }
}