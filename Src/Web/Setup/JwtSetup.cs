using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shortener.Infrastructure.Authentication.Jwt;

namespace Shortener.Web.Setup;

public class JwtSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration = configuration;
    private const string SectionName = "Jwt";

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
