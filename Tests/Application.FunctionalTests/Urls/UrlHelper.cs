using Shortener.Application.Urls.Commands.CreateUrl;

namespace Shortener.Tests.Application.FunctionalTests.Urls
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
