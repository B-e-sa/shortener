using Shortener.Application.Urls.Commands.CreateUrl;
using Shortener.Tests.Application.E2ETests.Abstractions;

namespace Shortener.Tests.Application.E2ETests.Urls
{
    public class UrlHelper() : TestHelper("url")
    {
        public CreateUrlCommand GenerateValidUrl()
        {
            return new CreateUrlCommand()
            {
                Title = faker.Lorem.Sentence(2),
                Url = faker.Internet.Url()
            };
        }
    }
}
