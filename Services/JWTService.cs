using Microsoft.IdentityModel.Tokens;
using PoC_Demo.Repository;
using PoC_Demo.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace PoC_Demo.Services
{
    public class JWTService : Connection, ITokenService
    {
        private readonly IConfiguration _configuration;


        public JWTService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken()
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinute"])),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var JWTToken = tokenHandler.WriteToken(token);

            return JWTToken;
        }
    }
}
