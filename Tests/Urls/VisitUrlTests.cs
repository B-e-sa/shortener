using System.Net;
using FluentAssertions;
using Shortener.Controllers;
using Shortener.Tests.Abstractions;
using Xunit;

namespace Shortener.Tests.Urls
{
    public class VisitUrlTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
    {
        private readonly TestHelper helper = new();

        [Fact]
        public async Task Should_RedirectAndIncreaseVisitCount_WhenUrlExists()
        {
            // Arrange
            var url = new UrlCreateRequest("New Url", "https://www.google.com");
            var createdRes = await HttpClient.PostAsJsonAsync("http://localhost:5229", url);
            var createdBody = await helper.DeserializeResponse<UrlCreateResponse>(createdRes);
            var shortUrl = $"http://localhost:5229/{createdBody.Data?.ShortUrl}";

            // Act
            await HttpClient.GetAsync(shortUrl);
            var visitRes = await HttpClient.GetAsync(shortUrl);

            // Assert
            var foundRes = await HttpClient.GetAsync($"http://localhost:5229/{createdBody.Data?.Id}");
            var foundResBody = await helper.DeserializeResponse<FindUrlByIdResponse>(foundRes);

            var expectedValues = new[]
            {
                HttpStatusCode.Redirect,
                HttpStatusCode.OK
            };
            visitRes.StatusCode.Should().BeOneOf(expectedValues);
            foundResBody.Data?.Visits.Should().Be(1);
        }
    }
}