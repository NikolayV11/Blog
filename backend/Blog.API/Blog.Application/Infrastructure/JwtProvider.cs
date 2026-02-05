using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Core.Abstractions.Auth;
using Blog.Core.Models;

namespace Blog.Application.Infrastructure {
    public class JwtProvider
        : IJwtProvaider
        {
        private readonly IConfiguration _configuration;

        public JwtProvider ( IConfiguration configuration ) {
            _configuration = configuration;
        }

        public string GenerateToken ( User user ) {
            var claims = new[] {
                new Claim("userId", user.Id.ToString() ),
                new Claim(ClaimTypes.Email, user.Email ),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
