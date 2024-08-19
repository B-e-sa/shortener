using FluentAssertions;
using Newtonsoft.Json;
using Shortener.Controllers;
using Shortener.Tests.Abstractions;
using Xunit;

namespace Shortener.Tests.Urls
{
    public class GetTopUrlsTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
    {
        private readonly TestHelper helper = new();

        [Fact]
        public async Task Should_ReturnTopUrls_OnRequest()
        {
            // Arrange
            var url = new UrlCreateRequest("New Url", "https://www.google.com");

            // Act
            for (int i = 0; i < 10; i++)
            {
                await HttpClient.PostAsJsonAsync("http://localhost:5229", url);
            }
            var res = await HttpClient.GetAsync("http://localhost:5229");

            // Assert
            var resBody = await helper.DeserializeResponse<GetTopUrlsResponse>(res);
            resBody.Data?.Count.Should().Be(10);
        }
    }
}