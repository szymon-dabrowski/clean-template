using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.Shared.Application.Setup;
using Clean.Modules.Shared.Infrastructure.ExecutionContext;
using Clean.Web.Api.Common.Endpoints;
using Clean.Web.Api.Common.Permissions;
using System.Reflection;

namespace Clean.Web.Api.Setup;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddEndpointsModules();

        services.AddHttpContextAccessor();

        services.AddSwagger();

        services.AddMappings(Assembly.GetExecutingAssembly());

        services.AddAuth(config);

        services.AddOutboxMessagesProcessingJob();

        services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

        services.AddPermissions();

        return services;
    }
}