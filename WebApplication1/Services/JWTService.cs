using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WhenUp;

namespace WebApplication1.Services
{
    public class JWTService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly WhenAppContext _context;

        public JWTService(WhenAppContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }

        public string CreateToken(string username)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:SecretKey"]));
            var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim("UserId", username)
            };

            var token = new JwtSecurityToken(
                _configuration["JWTParams:Issuer"],
                _configuration["JWTParams:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: mac
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ValidateToken()
        {
            return string.Empty;
        }

        public string GetUsernameFromToken()
        {
            return string.Empty;
        }
    }
}
