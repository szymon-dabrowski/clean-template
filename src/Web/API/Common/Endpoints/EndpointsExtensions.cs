namespace Clean.Web.Api.Common.Endpoints;

internal static class EndpointsExtensions
{
    public static IServiceCollection AddEndpointsModules(this IServiceCollection services)
    {
        services.Scan(s => s.FromAssemblyOf<IEndpointsModule>()
            .AddClasses(c => c.AssignableTo<IEndpointsModule>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        return services;
    }

    public static WebApplication UseEndpointsModules(this WebApplication app)
    {
        var endpointsModules = app.Services.GetServices<IEndpointsModule>();

        foreach (var endpointsModule in endpointsModules)
        {
            endpointsModule.RegisterEndpoints(app);
        }

        return app;
    }
}