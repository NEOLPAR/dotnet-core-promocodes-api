using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PromocodesApp.Helpers
{
    public class AuthenticationHelper
    {
        public static string GetToken(string authorization)
        {
            if (AuthenticationHeaderValue
                .TryParse(authorization, out var headerValue))
            {
                // we have a valid AuthenticationHeaderValue that has the following details:

                var scheme = headerValue.Scheme;
                return headerValue.Parameter;

                // scheme will be "Bearer"
                // parmameter will be the token itself.
            }
            return null;
        }
        
        public static string GetUser(string token, IConfiguration configuration)
        {
            if (token == null) return null;

            string secret = configuration["JWT:Secret"];
            var key = Encoding.UTF8.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            return claims.Identity.Name;
        }

        public static string GetUserFromToken(string authorization, IConfiguration configuration)
        {
            return GetUser(GetToken(authorization), configuration);
        }
    }
}
