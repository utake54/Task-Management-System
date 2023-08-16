using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace JWTAuth_Validation.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                ValidateToken(context, token, _configuration);

            await _next(context);
        }

        private void ValidateToken(HttpContext context, string token, IConfiguration configuration)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["JWT:Secret"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                context.Items["UserId"] = jwtToken.Claims.First(x => x.Type == "UserId").Value;
                context.Items["RoleId"] = jwtToken.Claims.First(x => x.Type == "RoleId").Value;
                context.Items["CompanyId"] = jwtToken.Claims.First(x => x.Type == "CompanyId").Value;
                context.Items["Role"] = jwtToken.Claims.First(x => x.Type == "Role").Value;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
