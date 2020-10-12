﻿using CorrelationId.DependencyInjection;
using CorrelationId.HttpClient;
using HomeWork_ToDos.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HomeWork_ToDos.API.Services
{
    /// <summary>
    /// Extension for IService collection for adding CorrelationId Handler and setting defaults.
    /// </summary>
    public static class CorrelationIdServiceExtension
    {
        public static IServiceCollection AddCorrelationIdHandlerAndDefaults(this IServiceCollection services)
        {
            services.AddTransient<CorrelationIdDelegatingHandler>();

            services.AddHttpClient("HomeWork_ToDos_Client")
                .AddCorrelationIdForwarding()           // add the handler to attach the correlation ID to outgoing requests for this named client
                .AddHttpMessageHandler<CorrelationIdDelegatingHandler>();

            services.AddDefaultCorrelationId(options =>
            {
                options.CorrelationIdGenerator = () => Guid.NewGuid().ToString();
                options.AddToLoggingScope = true;
                options.EnforceHeader = false;
                options.IgnoreRequestHeader = false;
                options.IncludeInResponse = true;
                options.RequestHeader = "Custom-Correlation-Id";
                options.ResponseHeader = "X-Correlation-Id";
                options.UpdateTraceIdentifier = false;
            });
            return services;
        }
    }
}
