using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Infrastructure;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Persistence;
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

        var assembly = typeof(DependencyInjection).Assembly;

        services.AddSingleton<IDateTimeProvider, TestDateTimeProvider>();
        services.AddSingleton<IExecutionContextAccessor, TestExecutionContextAccessor>();
        services.AddDbContext<DbContext, TestDbContext>(options =>
        {
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
        });
        services.AddScoped<IUnitOfWork, UoW>();
        services.AddScoped<ITestAggregateRootRepository, TestAggregateRootRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.DecorateCommandHandlersWithUnitOfWork();

        return services.BuildServiceProvider();
    }
}