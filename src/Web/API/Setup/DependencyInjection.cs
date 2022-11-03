using Clean.API.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Clean.API.Setup;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwagger();

        services.AddMappings();

        services.AddSingleton<ProblemDetailsFactory, CleanApiProblemDetailsFactory>();

        return services;
    }
}
