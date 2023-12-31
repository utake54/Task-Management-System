﻿using Microsoft.IdentityModel.Tokens;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Model.Model.Login.Response;
using TaskManagement.Model.Model.User;
using TaskManagement.Model.Model.Login.DTO;

namespace TaskManagement.API.Infrastructure
{
    public static class JWTHelper
    {
        public static LoginResponse Login(LoginDTO user)
        {
            var claims = new List<Claim>
                 {
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim("UserId", Convert.ToString(user.Id), ClaimValueTypes.Integer),
                    new Claim("RoleId", Convert.ToString(user.RoleId), ClaimValueTypes.Integer),
                    new Claim("CompanyId", Convert.ToString(user.CompanyId), ClaimValueTypes.Integer),
                    new Claim("Role", Convert.ToString(user.Role), ClaimValueTypes.Integer),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("UnBreakableJwTk3y"));
            var token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: DateTime.UtcNow.AddMinutes(60),
               signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
           );
            var response = new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
            return response;
        }
    }
}