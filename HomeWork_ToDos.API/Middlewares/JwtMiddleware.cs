using HomeWork_ToDos.CommonLib.Contracts.BL;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_ToDos.Middlewares
{
    /// <summary>
    /// Middleware for Jwt Token verification.
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Create new instance of <see cref="JwtMiddleware"/> class.
        /// </summary>
        /// <param name="next">Next request delegate.</param>
        /// <param name="appSettings">App seetings.</param>
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Process request.
        /// </summary>
        /// <param name="context">Http context.</param>
        /// <param name="userService">User service.</param>
        /// <returns>Returns nothing.</returns>
        public async Task Invoke(HttpContext context, IUserContract userService)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                await AttachUserToContext(context, userService, token);
            }
            await _next(context);
        }
        private async Task AttachUserToContext(HttpContext context, IUserContract userService, string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token = token.Replace("Bearer ", string.Empty);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret))
            }, out SecurityToken validatedToken);

            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

            //attach user to context on successful jwt validation
            long UserId = long.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);
            UserDto userDto = await userService.GetById(UserId);
            if (userDto == null)
            {
                throw new AccessViolationException();
            }
            context.Items["UserId"] = userDto.UserId;
        }
    }
    public static class JwtMiddlewareExtension
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
