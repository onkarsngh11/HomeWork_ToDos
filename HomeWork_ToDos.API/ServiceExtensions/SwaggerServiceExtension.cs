using HomeWork_ToDos.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;

namespace HomeWork_ToDos.API.Services
{
    public static class SwaggerServiceExtension
    {
        /// <summary>
        /// implements extension method for adding Sqagger services.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(p =>
            {
                p.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeWork ToDos API", Version = "v1" });
                p.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                p.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                p.OperationFilter<CorrelationIdOperationFilter>();
                p.SchemaFilter<ExampleSchemaFilter>();
                p.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                ////Set the comments path for the Swagger JSON and UI.

                string filePath = Path.Combine(System.AppContext.BaseDirectory, "HomeWork_ToDos.API.xml");
                p.IncludeXmlComments(filePath);
            });
            services.AddSwaggerGenNewtonsoftSupport();
            return services;
        }
    }
}
