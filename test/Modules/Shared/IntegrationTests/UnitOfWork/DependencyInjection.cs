using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Infrastructure.DependencyInjection;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Infrastructure;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Persistence;
using Clean.Modules.Shared.Persistence.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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
        services.AddMediatR(assembly);

        services
            .RegisterCommandHandlersAsClosedTypes(assembly)
            .DecorateCommandHandlersWithUnitOfWork();

        return services.BuildServiceProvider();
    }
}