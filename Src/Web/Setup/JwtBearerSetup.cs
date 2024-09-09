using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shortener.Infrastructure.Authentication.Jwt;
using System.Text;

namespace Shortener.Web.Setup;

public class JwtBearerSetup(IOptions<JwtOptions> options) : IConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _options = options.Value;

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            ValidIssuer = _options.Issuer,
            ValidAudience = _options.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret))
        };
    }
}