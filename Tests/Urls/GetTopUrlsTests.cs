using FluentAssertions;
using Shortener.Src.Application.Url.Commands.CreateUrl;
using Shortener.Src.Tests.Abstractions;
using Xunit;

namespace Shortener.Src.Tests.Urls
{
    public class GetTopUrlsTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
    {
        private readonly TestHelper helper = new("url");

        [Fact]
        public async Task Should_ReturnTopUrls_OnRequest()
        {
            // Arrange
            var url = new CreateUrlCommand()
            {
                Title = "New Url",
                Url = "https://www.google.com"
            };

            // Act
            for (int i = 0; i < 10; i++)
            {
                await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), url);
            }

            // TODO: assert that the results are being returned
            //       in descending order

            var res = await HttpClient.GetAsync(helper.GetApiUrl());

            // Assert
            var resBody = await helper.DeserializeResponse<List<Domain.Entities.Url>>(res);
            resBody.Count.Should().Be(10);
        }
    }
}