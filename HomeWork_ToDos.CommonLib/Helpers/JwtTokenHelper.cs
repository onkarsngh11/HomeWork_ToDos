using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HomeWork_ToDos.CommonLib.Helpers
{
    public static class JwtTokenHelper
    {
        /// <summary>
        /// Returns Jwt Token containing UserName,UserId,UserRole
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="appSettings"></param>
        /// <returns> Jwt Token</returns>
        public static string GenerateJwtToken(UserDto userDto, AppSettings appSettings)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Claim[] claims =
                new[]
                {
                    new Claim(ClaimTypes.Role, userDto.UserRole),
                    new Claim("UserId", Convert.ToString(userDto.UserId)),
                    new Claim("UserName",userDto.UserName),
                };
            JwtSecurityToken token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
