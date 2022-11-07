using Clean.API.Common.Errors;
using Clean.Web.API.Common.Setup;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Clean.Web.API;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwagger();

        services.AddMappings();

        services.AddSingleton<ProblemDetailsFactory, ApiProblemDetailsFactory>();

        return services;
    }
}
