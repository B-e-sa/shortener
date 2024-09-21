using Shortener.Domain.Entities;
using System.Security.Claims;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Shortener.Application.Common.Interfaces;
using System.Linq;

namespace Shortener.Infrastructure.Authentication.Jwt
{

    public sealed class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;
        private readonly JwtSecurityTokenHandler handler = new();


        public string Generate(User user)
        {
            var claims = new Claim[]
            {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email)
         };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_options.Secret)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                null,
                DateTime.UtcNow.AddDays(7),
                signingCredentials);

            return handler.WriteToken(token);
        }

        public ClaimDto Read(string token)
        {
            var jwtToken = handler.ReadJwtToken(token);

            var email = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email).Value;
            var id = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value;

            return new ClaimDto()
            {
                Email = email,
                Id = int.Parse(id)
            };
        }

        public bool Validate(string token)
        {
            try
            {
                var jwtToken = handler.ReadJwtToken(token);
                return jwtToken.ValidFrom < DateTime.UtcNow || jwtToken.ValidTo > DateTime.UtcNow;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
