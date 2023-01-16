using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.Shared.Infrastructure.ExecutionContext;
using Clean.Web.Api.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Clean.Web.Api.Setup;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddHttpContextAccessor();

        services.AddSwagger();

        services.AddMappings();

        services.AddSingleton<ProblemDetailsFactory, ApiProblemDetailsFactory>();

        services.AddAuth(config);

        services.AddOutboxMessagesProcessingJob();

        services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

        return services;
    }
}