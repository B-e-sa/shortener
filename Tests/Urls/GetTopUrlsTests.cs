using System.Net;
using FluentAssertions;
using Shortener.Tests.Abstractions;
using Xunit;

namespace Shortener.Tests.Urls
{
    public class GetTopUrlsTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
    {
        [Fact]
        public async Task Should_ReturnTopUrls_OnRequest()
        {
            // Act
            var res = await HttpClient.GetAsync("http://localhost:5229");

            // Assert
            res.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}