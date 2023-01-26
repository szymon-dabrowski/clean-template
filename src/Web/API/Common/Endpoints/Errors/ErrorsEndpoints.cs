using Microsoft.AspNetCore.Diagnostics;

namespace Clean.Web.Api.Common.Endpoints.Errors;

internal class ErrorsEndpoints : IEndpointsModule
{
    private const string ErrorsRoute = "/errors";

    public void RegisterEndpoints(WebApplication app)
    {
        app.UseExceptionHandler(ErrorsRoute);

        app.MapGet(ErrorsRoute, (HttpContext context) =>
        {
            var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            return Results.Problem(title: exception?.Message);
        });
    }
}