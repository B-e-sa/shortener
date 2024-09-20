using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shortener.Infrastructure.Mailing;

namespace Shortener.Web.Setup
{
    public class MailingSetup(IConfiguration configuration) : IConfigureOptions<MailingOptions>
    {
        private readonly IConfiguration _configuration = configuration;
        private const string SectionName = "Smtp";

        public void Configure(MailingOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
