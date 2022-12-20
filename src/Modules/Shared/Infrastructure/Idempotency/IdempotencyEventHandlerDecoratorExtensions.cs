﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Shared.Infrastructure.Idempotency;

public static class IdempotencyEventHandlerDecoratorExtensions
{
    public static IServiceCollection DecorateEventHandlersWithIdempotency(
        this IServiceCollection services)
    {
        services.Decorate(
            typeof(INotificationHandler<>),
            typeof(IdempotencyEventHandlerDecorator<>));

        return services;
    }
}