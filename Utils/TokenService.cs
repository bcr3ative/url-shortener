using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UrlShortener.Models;

namespace UrlShortener.Utils
{
    // Class for generating authentication tokens
    public static class TokenService
    {
        // Random generated private key/secret
        // Must be changed before publishing
        public static string PRIVATE_KEY = "24U+?p?9cbW%%kPDe@3b%zzD+qYW7R#6b4uHj5Yy";

        // Expire date for token is 1 hour
        private const double EXPIRE_HOURS = 1.0;

        public static string CreateToken(Account acc)
        {
            // Encode the private key
            var key = Encoding.ASCII.GetBytes(TokenService.PRIVATE_KEY);

            var tokenHandler = new JwtSecurityTokenHandler();

            // Setup token descriptor with necessary information
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, acc.AccountId.ToString()),
                    new Claim(ClaimTypes.Role, "User")
                }),
                Expires = DateTime.UtcNow.AddHours(EXPIRE_HOURS),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Generate token based on provided properties
            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
