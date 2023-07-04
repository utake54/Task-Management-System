using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TaskManagement.API.Infrastructure.Middleware
{
    public class JWTValidator
    {
        public bool ValidateToken(string token, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                ValidateIssuer = false, // Set to true if you want to validate the issuer
                ValidateAudience = false, // Set to true if you want to validate the audience
                RequireExpirationTime = true,
                ValidateLifetime = true
            };

            try
            {
                // Validate the token and return the result
                tokenHandler.ValidateToken(token, validationParameters, out _);
                return true;
            }
            catch (Exception)
            {
                // Token validation failed
                return false;
            }
        }

    }
}
