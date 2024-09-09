using Shortener.Application.Users.Abstractions;
using Shortener.Domain.Entities;
using System.Security.Claims;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace Shortener.Infrastructure.Authentication.Jwt
{

    internal sealed class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        public string Generate(User user)
        {
            if (string.IsNullOrEmpty(_options.Secret))
            {
                throw new ArgumentNullException(nameof(_options.Secret), "JWT Secret is not set.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
                ]);

            var signinCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret)),
                    SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                SigningCredentials = signinCredentials,
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                Expires = DateTime.UtcNow.AddDays(7),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
