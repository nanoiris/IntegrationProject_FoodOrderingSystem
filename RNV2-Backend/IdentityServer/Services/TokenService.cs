using IdentityServer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityServer.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(AppUser user, IConfiguration configuration)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
                new Claim("UserName", user.UserName.ToString()),
                new Claim("UserEmail", user.Email),
            };
            if (user.District != null)
                claims.Add(new Claim("UserDistrict", user.District));
            if (user.RestaurantId != null)
                claims.Add(new Claim("UserRestId", user.RestaurantId.ToString()));
            if (user.RestaurantName != null)
                claims.Add(new Claim("UserRestName", user.RestaurantName));
            if (user.Role != null)
                claims.Add(new Claim("UserRole", user.Role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["AuthSettings:Issuer"],
                audience: configuration["AuthSettings:Audince"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds,
                claims: claims
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
