﻿using Clean.API.Common.Errors;
using Clean.Modules.Shared.Infrastructure.Outbox;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Clean.Web.API.Setup;

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