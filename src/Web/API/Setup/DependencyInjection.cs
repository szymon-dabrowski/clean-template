using Clean.Api.Common.Errors;
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

        services.AddSwagger();

        services.AddMappings();

        services.AddSingleton<ProblemDetailsFactory, ApiProblemDetailsFactory>();

        services.AddAuth(config);

        services.AddOutboxMessagesProcessingJob();

        return services;
    }
}