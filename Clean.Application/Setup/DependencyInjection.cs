﻿using Clean.Application.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Application.Setup;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
