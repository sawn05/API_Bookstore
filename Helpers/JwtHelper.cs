using API_Bookstore.Configurations;
using API_Bookstore.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Bookstore.Helpers
{
    public class JwtHelper
    {
        private readonly JwtSettings _jwtSettings;

        public JwtHelper(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Username", user.Username),
                new Claim("Id", user.Id.ToString()),

                new Claim("TokenId", Guid.NewGuid().ToString())
            };

            var secretKeyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var expires = DateTime.UtcNow.AddHours(1);

            // Ký thông tin xác thực
            var creds = new SigningCredentials(secretKeyBytes, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
