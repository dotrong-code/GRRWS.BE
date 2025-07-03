using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GRRWS.Application.Implement.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<SecurityToken> GenerateTokenAsync(CurrentUserObject currentUserObject)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // Retrieve the secret key from configuration
            var secretKey = _configuration["Jwt:Key"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);


            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, currentUserObject.UserId.ToString()),  // Ensure UserId is string
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, currentUserObject.Fullname),
            new Claim("phoneNumber", currentUserObject.PhoneNumber),
            new Claim("role", currentUserObject.RoleName),
            new Claim(ClaimTypes.Email, currentUserObject.Email),
            new Claim("userId", currentUserObject.UserId.ToString())  // UserId to string to avoid type conflicts
        }),
                Expires = TimeHelper.GetHoChiMinhTime().AddMinutes(180),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            // Since no truly async operation is involved, return Task.FromResult for consistency
            return await Task.FromResult(token);
        }

        public Task<dynamic> RenewTokenAsync(RenewTokenDTO tokenDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<string> GenerateAccessTokenAsync(SecurityToken token)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            return await Task.FromResult(jwtTokenHandler.WriteToken(token));
        }
    }
}
