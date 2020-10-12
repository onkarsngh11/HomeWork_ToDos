using CorrelationId.Abstractions;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HomeWork_ToDos.API.Middlewares
{
    /// <summary>
    /// Exception Middleware.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContextAccessor;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, ICorrelationContextAccessor correlationContextAccessor)
        {
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
            _next = next;
            _correlationContextAccessor = correlationContextAccessor;
        }

        /// <summary>
        /// Process request.
        /// </summary>
        /// <param name="httpContext">HttpContext.</param>
        /// <returns>Returns nothing.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        /// <summary>
        /// Handles exception and adds message in response with status code .
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string correlationId = _correlationContextAccessor.CorrelationContext.CorrelationId;
            _logger.LogError($"Error: {exception}, Correlation id: {correlationId}");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            }.ToString());
        }
    }
    /// <summary>
    /// Extension of application builder for exception middleware.
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Configure exception middleware.
        /// </summary>
        /// <param name="app">Application builder.</param>
        public static IApplicationBuilder ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }

    }
}
