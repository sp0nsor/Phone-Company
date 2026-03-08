using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhoneCompany.Application.Interfaces.Internal;
using PhoneCompany.Application.Options;
using PhoneCompany.Infrastructure.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PhoneCompany.Application.Services.Internal
{
    internal class AccessTokenService : IAccessTokenService
    {
        private readonly AccessTokenOptions _options;

        public AccessTokenService(IOptions<AccessTokenOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateAccessToken(UserEntity user)
        {
            var jwtTokenId = Guid.NewGuid().ToString();

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, jwtTokenId),
                new(ClaimTypes.Role, user.Role.Name),
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                issuer: _options.Issuer,
                expires: DateTime.UtcNow.AddMinutes(
                    _options.ExpiresMinutes));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
