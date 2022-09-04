using Clean.API.Common.Errors;
using Clean.API.Common.Mappings;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Clean.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddMappings();

        services.AddSingleton<ProblemDetailsFactory, CleanApiProblemDetailsFactory>();

        return services;
    }
}
